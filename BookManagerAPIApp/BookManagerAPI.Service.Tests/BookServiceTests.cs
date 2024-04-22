using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Repository.Models;
using BookManagerAPI.Repository.Models.ResponseModels;
using BookManagerAPI.Service.Impl;
using BookManagerAPI.Service.Interfaces;
using BookManagerAPI.Service.Models.Book;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Tests
{
    [TestFixture]
    public class BookServiceTests
    {
        [Test]
        public async Task SaveImageToBlobAndAddNewBook_When_Called_Adds_New_Book()
        {
            //Arrange
            Mock<ISQLDBRepository> _mockSQLDBRepository = new Mock<ISQLDBRepository>();
            Mock<IAzureBlobRepository> _mockAzureBlobRepository = new Mock<IAzureBlobRepository>();
            Mock<ILogger<BookService>> _mockBookServiceLogger = new Mock<ILogger<BookService>>();

            UploadImageToBlobAsyncResponseModel uploadImageToBlobAsyncResponseModel = new(true,string.Empty, "sampleBlob");
            AddBookModel addBookModel = new AddBookModel() { Name = "Sample", Price = 7.25M, CategoryId = 1, ImageBlobURL = "sampleURL", PurchasedDate = It.IsAny<DateTime>() };

            _mockAzureBlobRepository.Setup(x => x.UploadImageToBlobAsync(It.IsAny<IFormFile>())).Returns(async () => uploadImageToBlobAsyncResponseModel);

            _mockSQLDBRepository.Setup(x => x.AddNewBook(addBookModel)).Returns(async () => true);

            IFormFile formFile = new FormFile(new MemoryStream(), 0, 11, "", "");

            BookRequestModel bookModel = new() { Name = "Sample", Price = 7.25M, CategoryId = 1, PurchasedDate = It.IsAny<DateTime>(), Image = formFile };

            // Act
            IBookService bookService = new BookService(_mockSQLDBRepository.Object,_mockAzureBlobRepository.Object,_mockBookServiceLogger.Object);
            var response = await bookService.SaveImageToBlobAndAddNewBook(bookModel);

            //Assert
            Assert.That(response.IsSuccess, Is.True);
        }
    }
}
