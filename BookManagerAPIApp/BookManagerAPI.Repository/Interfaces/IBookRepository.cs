using BookManagerAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Interfaces
{
    public interface IBookRepository
    {
        public Task<bool> AddNewBook(AddBookModel model);
    }
}
