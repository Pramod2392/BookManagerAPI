namespace BookManagerAPI.Models;

public class Book
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime PurchasedDate { get; set; }

    public float Price { get; set; }

    public string? ImageBlobURL { get; set; }

    public int CategoryId { get; set; }
}