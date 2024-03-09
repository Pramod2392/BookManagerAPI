﻿namespace BookManagerAPI.Repository.Models;

public class UserModel
{
    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public string? DisplayName { get; set; }

    public DateTime? CreatedDate { get; set; }
}
