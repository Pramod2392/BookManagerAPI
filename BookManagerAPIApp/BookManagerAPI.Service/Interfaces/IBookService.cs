using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Interfaces
{
    public interface IBookService
    {
        public Task SaveImageToBlobAndAddNewBook();
    }
}
