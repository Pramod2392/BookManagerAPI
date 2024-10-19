using BookManagerAPI.Repository.Models;
using BookManagerAPI.Service.Models;
using BookManagerAPI.Service.Models.Book;
using BookManagerAPI.Service.Models.Pagination;
using BookManagerAPI.Service.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Interfaces
{
    public interface IBookService
    {
        public Task<SaveImageToBlobAndAddNewBookResponseModel> SaveImageToBlobAndAddNewBook(BookRequestModel bookModel);
        Task<ServiceResponse<PagedGetBookModel>> GetAllBooks(PaginationModel paginationModel, string searchText);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Language>> GetAllLanguages();
    }
}
