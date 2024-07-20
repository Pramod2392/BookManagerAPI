namespace BookManagerAPI.Web.Contracts.Category
{
    public class GetAllCategoryResponseModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
