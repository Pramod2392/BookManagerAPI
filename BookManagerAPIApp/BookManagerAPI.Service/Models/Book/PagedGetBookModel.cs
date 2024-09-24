using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Models.Book
{
    public class PagedGetBookModel
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<GetBookModel> Books { get; set; } = Enumerable.Empty<GetBookModel>().ToList();
    }
}
