﻿using AutoMapper;
using Azure.Storage.Blobs.Models;
using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Repository.Models;
using BookManagerAPI.Service.Interfaces;
using BookManagerAPI.Service.Models;
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
        private readonly IMapper _mapper;

        public BookService(ISQLDBRepository bookRepository, IAzureBlobRepository azureBlobRepository, ILogger<BookService> logger, IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            this._bookRepository = bookRepository;
            this._azureBlobRepository = azureBlobRepository;
            this._logger = logger;            
            this._httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
        }
        public async Task<SaveImageToBlobAndAddNewBookResponseModel> SaveImageToBlobAndAddNewBook(BookRequestModel bookModel)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).Single().Value;

                var response = await _azureBlobRepository.UploadImageToBlobAsync(bookModel.Image, userId);
                if (response.IsSuccess)
                {
                    AddBookModel addBookModel = new() { CategoryId = bookModel.CategoryId, ImageBlobURL = response.BlobName, Name = bookModel.Name, Price = bookModel.Price, PurchasedDate = bookModel.PurchasedDate };
                    var addBookResult = await _bookRepository.AddNewBook(addBookModel);

                    if (addBookResult?.Id <= 0)
                    {
                        return new SaveImageToBlobAndAddNewBookResponseModel(false, "Error while adding book");
                    }

                    await _bookRepository.AddBookUserMap(new AddBookUserMap() { UserId = new Guid(userId), BookId = addBookResult.Id });
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

        public async Task<ServiceResponse<IEnumerable<GetBookModel>>> GetAllBooks()
        {
            try
            {
                var bookList = new List<GetBookModel>();

                // Get UserId from token
                var userId = GetUserIdFromToken();

                // Get All BooksIds for the given user Id from BookUserMap table
                // Get All Books for the given Ids from Book table

                //var booksList = await _bookRepository.GetAllBooksForGivenUserId(new System.Data.SqlTypes.SqlGuid(userId));
                var booksList = await _bookRepository.GetAllBooksForGivenUserId(new Guid(userId));

                // Get image from blob url

                foreach (var book in booksList)
                {
                    bookList.Add(new GetBookModel() { Image = await _azureBlobRepository.GetImageFromBlob(book.ImageBlobURL),
                                                      Id = book.Id, Title = book.Name});
                }

                return new ServiceResponse<IEnumerable<GetBookModel>>(bookList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching books");
                return new ServiceResponse<IEnumerable<GetBookModel>>("Error while fetching books");                
            }
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
