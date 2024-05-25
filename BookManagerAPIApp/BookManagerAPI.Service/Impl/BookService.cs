using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Repository.Models;
using BookManagerAPI.Service.Interfaces;
using BookManagerAPI.Service.Models.Book;
using BookManagerAPI.Service.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Impl
{
    public class BookService : IBookService
    {
        private readonly ISQLDBRepository _bookRepository;
        private readonly IAzureBlobRepository _azureBlobRepository;
        private readonly ILogger<BookService> _logger;        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookService(ISQLDBRepository bookRepository, IAzureBlobRepository azureBlobRepository, ILogger<BookService> logger, IHttpContextAccessor httpContextAccessor)
        {
            this._bookRepository = bookRepository;
            this._azureBlobRepository = azureBlobRepository;
            this._logger = logger;            
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<SaveImageToBlobAndAddNewBookResponseModel> SaveImageToBlobAndAddNewBook(BookRequestModel bookModel)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type.Equals("sub")).Single().Value;

                var response = await _azureBlobRepository.UploadImageToBlobAsync(bookModel.Image, userId);
                if (response.IsSuccess)
                {
                    AddBookModel addBookModel = new() { CategoryId = bookModel.CategoryId, ImageBlobURL = response.BlobName, Name = bookModel.Name, Price = bookModel.Price, PurchasedDate = bookModel.PurchasedDate };
                    await _bookRepository.AddNewBook(addBookModel);
                    await _bookRepository.AddBookUserMap(new AddBookUserMap() { UserId = new System.Data.SqlTypes.SqlGuid(new Guid(userId)), BookId = 1 });
                    return new SaveImageToBlobAndAddNewBookResponseModel(true, "Book successfully added");
                }
                else
                {
                    _logger.LogError("Error whie uploading image to blob");
                    return new SaveImageToBlobAndAddNewBookResponseModel(false, "Error whie uploading image to blob");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding new book");
                throw;
            }
        }

        public async Task<IEnumerable<GetBookModel>> GetAllBooks()
        {
            try
            {
                //Get UserId from token
                var userId = GetUserIdFromToken();

                // Get All the books for the given user id from SQL


                // Fetch the image from azure blob using blob url
                await Task.CompletedTask;


                return new List<GetBookModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching books");
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
