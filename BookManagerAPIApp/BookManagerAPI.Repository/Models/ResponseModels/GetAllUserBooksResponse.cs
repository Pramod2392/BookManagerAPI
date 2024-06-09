using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Models.ResponseModels
{
    public class GetAllUserBooksResponse
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public string ImageBlobURL { get; set; }
        public decimal Price { get; set; }
        public CategoryModel Category { get; set; }                                                 
        public DateTime PurchasedDate { get; set; }
    }
}
