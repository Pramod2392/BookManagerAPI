using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Models
{
    public class AddBookUserMap
    {
        public int BookId { get; set; }

        public Guid UserId { get; set; }
    }
}
