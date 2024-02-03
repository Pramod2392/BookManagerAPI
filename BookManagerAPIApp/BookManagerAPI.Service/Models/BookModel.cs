using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Models
{
    public class BookModel
    {
        public string Name { get; set; }
        public DateOnly PurchasedDate { get; set; }
        public decimal Price { get; set; }
        public string ImageBlobURL { get; set; }
        public int CategoryId { get; set; }
    }
}
