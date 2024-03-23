using BookManagerAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Interfaces
{
    public interface ISQLDBRepository
    {
        public Task<bool> AddNewBook(AddBookModel model);

        public Task<bool> AddNewUser(AddUserModel userModel);
    }
}
