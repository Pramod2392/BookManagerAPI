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
        public Task<bool> AddNewBook(AddBookModel model);

        public Task<bool> AddNewUser(AddUserModel userModel);

        public Task<bool> AddBookUserMap(AddBookUserMap addBookUserMap);

        public Task<IEnumerable<GetAllUserBooksResponse>> GetAllBooksForGivenUserId(Guid userId);
    }
}
