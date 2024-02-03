using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Impl
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }
        public Task SaveImageToBlobAndAddNewBook()
        {
            throw new NotImplementedException();
        }
    }
}
