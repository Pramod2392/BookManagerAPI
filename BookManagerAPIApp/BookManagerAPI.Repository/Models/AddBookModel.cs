﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Models
{
    public class AddBookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PurchasedDate { get; set; }
        public decimal Price { get; set; }
        public string ImageBlobURL { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
    }
}
