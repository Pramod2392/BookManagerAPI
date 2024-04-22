namespace BookManagerAPI.Web.Contracts.Book
{
    public class BookRequestModel
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }        
    }
}
