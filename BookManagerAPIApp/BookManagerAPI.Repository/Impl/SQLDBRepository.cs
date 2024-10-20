﻿using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Repository.Models;
using BookManagerAPI.Repository.Models.ResponseModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Impl
{
    public class SQLDBRepository : ISQLDBRepository
    {
        private readonly ILogger<SQLDBRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public SQLDBRepository(ILogger<SQLDBRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            this._configuration = configuration;
            _connection = new SqlConnection(GetConnectionString());
        }

        private string? GetConnectionString() 
        {
            return Convert.ToString(_configuration["SQLDBConnectionString"]);
        }

        public async Task<AddBookModel?> AddNewBook(AddBookModel model)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    var queryResult = await connection.QueryAsync<AddBookModel>("dbo.AddBook @name, @purchasedDate, @price, @imageBlobURL, @categoryId, @languageId",
                                    new { name = model.Name, purchasedDate = model.PurchasedDate, price = model.Price, imageBlobURL = model.ImageBlobURL, categoryId = model.CategoryId, languageId = model.LanguageId });
                    return queryResult == null ? new AddBookModel() : queryResult.FirstOrDefault();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while adding new book to database");
                throw;
            }            
        }

        public async Task<bool> AddNewUser(AddUserModel userModel)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    var queryResult = await _connection.QueryAsync<bool>("AddUser @userId, @firstName, @lastName, @displayName, @emailId",
                                                    new { userId = userModel.UserId, firstName = userModel.FirstName, lastName = userModel.LastName, displayName = userModel.DisplayName, emailId = userModel.EmailId });

                    return true; 
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while adding new user to database");
                throw;
            }
        }

        public async Task<bool> AddBookUserMap(AddBookUserMap addBookUserMap)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    var queryResult = await connection.QueryAsync<bool>("dbo.AddBookUserMap @bookId, @userId", new { bookId = addBookUserMap.BookId, userId = addBookUserMap.UserId });
                    return true; 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding book-user map");
                throw;
            }
        }

        public async Task<IEnumerable<GetAllUserBooksResponse>> GetAllBooksForGivenUserId(Guid userId)
        {
            try
            {
                var queryResult = await _connection.QueryAsync<GetAllUserBooksResponse>("dbo.GetBooks @userId", new { userId = userId });
                return queryResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching books");
                throw;
            }
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            try
            {
                using var connection = new SqlConnection(GetConnectionString());
                var queryResult = await connection.QueryAsync<CategoryModel>("dbo.GetAllCategories");
                return queryResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Categories");
                throw;
            }
        }

        public async Task<IEnumerable<LanguageModel>> GetAllLanguages()
        {
            try
            {
                using var connection = new SqlConnection(GetConnectionString());
                var queryResult = await connection.QueryAsync<LanguageModel>("dbo.GetAllLanguages");
                return queryResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching languages");
                throw;
            }
        }
    }
}
