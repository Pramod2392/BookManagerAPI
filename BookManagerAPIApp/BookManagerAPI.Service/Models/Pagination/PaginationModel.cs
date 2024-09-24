namespace BookManagerAPI.Service.Models.Pagination
{
    public class PaginationModel
    {
        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;
    }
}
