namespace BookManagerAPI.Repository.Models;

public class AddUserModel
{
    public long UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public string? DisplayName { get; set; }

    public DateTime? CreatedDate { get; set; }
        
    public string EmailId { get; set; } = string.Empty;
}
