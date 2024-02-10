using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Repository.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Impl
{
    public class SQLDBReposiory : ISQLDBRepository
    {
        private readonly ILogger<SQLDBReposiory> _logger;

        public SQLDBReposiory(ILogger<SQLDBReposiory> logger)
        {
            this._logger = logger;
        }
        public async Task<bool> AddNewBook(AddBookModel model)
        {
            try
            {
                using IDbConnection connection = new SqlConnection("");
                var queryResult = await connection.QueryAsync("dbo.spo.AddBook @name, @purchasedDate, @price, @imageBlobURL, @categoryId",
                                    new { name = model.Name, purchasedDate = model.PurchasedDate, price = model.Price, imageBlobURL = model.ImageBlobURL, categoryId = model.CategoryId });
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while adding new book to database");
                return false;
            }            
        }
    }
}
