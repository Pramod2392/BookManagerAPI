using System.Text.Json.Serialization;

namespace BookManagerAPI.Web.Contracts.Book
{
    public class BookRequestModel
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public DateTime PurchasedDate { get; set; } = DateTime.Now;
    }
}
