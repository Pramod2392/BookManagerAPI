using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BookManagerAPI.Repository.Impl;
using BookManagerAPI.Repository.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Dapper;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace BookManagerAPI.Repository.Tests
{
    [TestFixture]
    public class SQLDBRepositoryTests
    {
        //[Test]
        //public async Task AddNewBook_When_Called_With_Valid_Request_Returns_True()
        //{
        //    //Arrange
        //    Mock<IDbConnection> moqSQLDBConnection = new Mock<IDbConnection>();
        //    Mock<IConfiguration> moqIConfiguration = new Mock<IConfiguration>();
        //    Mock<ILogger<SQLDBRepository>> moqLogger = new Mock<ILogger<SQLDBRepository>>();
        //    AddBookModel addBookModel = new AddBookModel() { CategoryId = 1, ImageBlobURL = "http://sampleurl", Name = "Winning it my way", Price = 125.23M, PurchasedDate = DateTime.Now };
        //    moqSQLDBConnection.SetupDapperAsync(x => x.QueryAsync<bool>(It.IsAny<string>(), new { name = addBookModel.Name, purchasedDate = addBookModel.PurchasedDate, price = addBookModel.Price, imageBlobURL = addBookModel.ImageBlobURL, categoryId = addBookModel.CategoryId }, It.IsAny<IDbTransaction>(), It.IsAny<int>(), It.IsAny<CommandType>())).Returns(() => true);

        //    //Act
        //    SQLDBRepository _sqlDBRepository = new SQLDBRepository(moqLogger.Object, moqIConfiguration.Object);
        //    var result = await _sqlDBRepository.AddNewBook(addBookModel);

        //    //Assert
        //    Assert.That(result, Is.EqualTo(true));
        //}

        //[Test]
        //public void AddNewBook_When_Called_Throws_Exception_If_There_Is_Exception_FromDB()
        //{
        //    //Arrange
        //    Mock<IDbConnection> moqSQLDBConnection = new Mock<IDbConnection>();
        //    Mock<IConfiguration> moqConfiguration = new Mock<IConfiguration>();
        //    Mock<ILogger<SQLDBRepository>> moqLogger = new Mock<ILogger<SQLDBRepository>>();
        //    AddBookModel addBookModel = new AddBookModel() { CategoryId = 1, ImageBlobURL = "http://sampleurl", Name = "Winning it my way", Price = 125.23M, PurchasedDate = DateTime.Now };
        //    moqSQLDBConnection.SetupDapperAsync(x => x.QueryAsync<bool>("dbo.spo.AddBook @name, @purchasedDate, @price, @imageBlobURL, @categoryId", addBookModel, null, null, It.IsAny<CommandType>())).Throws<Exception>();

        //    //Act and Assert
        //    SQLDBRepository _sqlDBRepository = new SQLDBRepository(moqLogger.Object, moqConfiguration.Object);
        //    Assert.ThrowsAsync<Exception>(async () => await _sqlDBRepository.AddNewBook(addBookModel));

        //}
    }
}
