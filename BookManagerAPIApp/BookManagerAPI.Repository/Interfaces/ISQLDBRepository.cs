using BookManagerAPI.Repository.Models;
using BookManagerAPI.Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Interfaces
{
    public interface ISQLDBRepository
    {
        Task<AddBookModel?> AddNewBook(AddBookModel model);

        Task<bool> AddNewUser(AddUserModel userModel);

        Task<bool> AddBookUserMap(AddBookUserMap addBookUserMap);

        Task<IEnumerable<GetAllUserBooksResponse>> GetAllBooksForGivenUserId(Guid userId);

        Task<IEnumerable<CategoryModel>> GetAllCategories();
    }
}
