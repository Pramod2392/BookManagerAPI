using AutoMapper;
using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Repository.Models;
using BookManagerAPI.Service.Interfaces;
using BookManagerAPI.Service.Models;
using BookManagerAPI.Service.Models.Book;
using BookManagerAPI.Service.Models.Pagination;
using BookManagerAPI.Service.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace BookManagerAPI.Service.Impl
{
    public class BookService : IBookService
    {
        private readonly ISQLDBRepository _bookRepository;
        private readonly IAzureBlobRepository _azureBlobRepository;
        private readonly ILogger<BookService> _logger;        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly string CacheBookListKeySuffix = "_BooksList";    
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;

        public BookService(ISQLDBRepository bookRepository, IAzureBlobRepository azureBlobRepository, ILogger<BookService> logger, IHttpContextAccessor httpContextAccessor,
            IMapper mapper, IMemoryCache memoryCache)
        {
            this._bookRepository = bookRepository;
            this._azureBlobRepository = azureBlobRepository;
            this._logger = logger;            
            this._httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
            this._memoryCache = memoryCache;
            this._cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2),
            };
        }
        public async Task<SaveImageToBlobAndAddNewBookResponseModel> SaveImageToBlobAndAddNewBook(BookRequestModel bookModel)
        {
            try
            {                
                var userId = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).Single().Value;

                // Check if book already exists. If exists, return conflict
                var cacheKey = GetCacheKey(userId);
                List<string> booksList = new();
                // Check the inmemory cache, if the cache is empty, then fetch all the books from db
                if (_memoryCache.TryGetValue(cacheKey, out booksList))
                {
                    if (booksList.Any(x => x.Equals(bookModel.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        return new SaveImageToBlobAndAddNewBookResponseModel(false, "Book already exists", System.Net.HttpStatusCode.Conflict);
                    }
                }
                else
                {
                    var bookListFromDB = await _bookRepository.GetAllBooksForGivenUserId(new Guid(userId));
                    if (bookListFromDB.Any(x => x.Name.Equals(bookModel.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        return new SaveImageToBlobAndAddNewBookResponseModel(false, "Book already exists", System.Net.HttpStatusCode.Conflict);
                    }
                    booksList = bookListFromDB.Select(x => x.Name).ToList();
                }

                var response = await _azureBlobRepository.UploadImageToBlobAsync(bookModel.Image, userId);
                if (response.IsSuccess)
                {
                    AddBookModel addBookModel = new() { CategoryId = bookModel.CategoryId, ImageBlobURL = response.BlobName, Name = bookModel.Name, Price = bookModel.Price, PurchasedDate = bookModel.PurchasedDate, LanguageId = bookModel.LanguageId };
                    var addBookResult = await _bookRepository.AddNewBook(addBookModel);

                    if (addBookResult?.Id <= 0)
                    {
                        return new SaveImageToBlobAndAddNewBookResponseModel(false, "Error while adding book", System.Net.HttpStatusCode.InternalServerError);
                    }

                    // Update cache
                    booksList.Add(addBookResult?.Name);
                    _memoryCache.Set(GetCacheKey(userId), booksList, _cacheEntryOptions);

                    await _bookRepository.AddBookUserMap(new AddBookUserMap() { UserId = new Guid(userId), BookId = addBookResult.Id });
                    return new SaveImageToBlobAndAddNewBookResponseModel(true, "Book successfully added", System.Net.HttpStatusCode.Created);
                }
                else
                {
                    _logger.LogError("Error whie uploading image to blob");
                    return new SaveImageToBlobAndAddNewBookResponseModel(false, "Error whie uploading image to blob", System.Net.HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding new book");
                throw;
            }
        }

        public async Task<ServiceResponse<PagedGetBookModel>> GetAllBooks(PaginationModel paginationModel, string searchText)
        {
            try
            {
                var bookList = new List<GetBookModel>();

                // Get UserId from token
                var userId = GetUserIdFromToken();

                // Get All BooksIds for the given user Id from BookUserMap table
                // Get All Books for the given Ids from Book table

                //var booksList = await _bookRepository.GetAllBooksForGivenUserId(new System.Data.SqlTypes.SqlGuid(userId));
                var books = await _bookRepository.GetAllBooksForGivenUserId(new Guid(userId));

                if (books == null)
                {
                    return new ServiceResponse<PagedGetBookModel>("Error fetching books from DB");
                }

                // Add the list of book names to InMemoryCache, only if the cache key is not found
                List<string> booksList = new ();
                string cacheKey = GetCacheKey(userId);
                List<string> bookNames = new();
                bookNames = books.Select(book => book.Name).ToList();
                if (!_memoryCache.TryGetValue(cacheKey, out booksList))
                {
                    _memoryCache.Set(cacheKey, bookNames, _cacheEntryOptions);
                }

                // search
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    books = books?.Where(x => x.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // pagination
                var pagedBookList = books?.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize).Take(paginationModel.PageSize);

                // Get image from blob url

                foreach (var book in pagedBookList)
                {
                    bookList.Add(new GetBookModel()
                    {
                        Image = await _azureBlobRepository.GetImageFromBlob(book.ImageBlobURL),
                        Id = book.Id,
                        Title = book.Name
                    });
                }

                PagedGetBookModel pagedGetBookModel = new()
                {
                    PageNumber = paginationModel.PageNumber,
                    PageSize = paginationModel.PageSize,
                    TotalCount = books?.Count() > 0 ? books.Count() : 0,
                    Books = bookList
                };


                return new ServiceResponse<PagedGetBookModel>(pagedGetBookModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching books");
                return new ServiceResponse<PagedGetBookModel>("Error while fetching books");                
            }
        }

        private string GetCacheKey(string userId)
        {
            return $"{userId}{CacheBookListKeySuffix}";
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var categoriesList = await _bookRepository.GetAllCategories();
                var categories = _mapper.Map<IEnumerable<Category>>(categoriesList);
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching categories");
                throw;
            }
        }

        public async Task<IEnumerable<Language>> GetAllLanguages()
        {
            try
            {
                var languageList = await _bookRepository.GetAllLanguages();
                var languages = _mapper.Map<IEnumerable<Language>>(languageList);
                return languages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching languages");
                throw;
            }
        }

        private string GetUserIdFromToken()
        {
            var _bearerToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var _token = _bearerToken.ToString().Split(' ')[1];
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenObject = jwtSecurityTokenHandler.ReadJwtToken(_token);
            var userId = jwtTokenObject.Claims.Where(x => x.Type.Equals("sub")).Single().Value;
            return userId;
        }
    }
}
