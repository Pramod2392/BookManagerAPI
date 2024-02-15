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
        public DateTime PurchasedDate { get; set; }
        public decimal Price { get; set; }
        public byte[] imageData { get; set; }
        public int CategoryId { get; set; }
    }
}
