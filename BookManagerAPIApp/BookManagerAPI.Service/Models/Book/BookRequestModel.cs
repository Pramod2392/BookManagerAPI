using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Models.Book
{
    public class BookRequestModel
    {
        public string Name { get; set; }
        public DateTime PurchasedDate { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }
    }
}
