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
    public class SQLDBRepository : ISQLDBRepository
    {
        private readonly ILogger<SQLDBRepository> _logger;
        private readonly IDbConnection _connection;

        public SQLDBRepository(ILogger<SQLDBRepository> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }
        public async Task<bool> AddNewBook(AddBookModel model)
        {
            try
            {                
                var queryResult = await _connection.QueryAsync("dbo.spo.AddBook @name, @purchasedDate, @price, @imageBlobURL, @categoryId",
                                    new { name = model.Name, purchasedDate = model.PurchasedDate.ToDateTime(TimeOnly.MinValue), price = model.Price, imageBlobURL = model.ImageBlobURL, categoryId = model.CategoryId });
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
