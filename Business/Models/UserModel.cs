﻿using Infrastructure.Entitites;

namespace Business.Models;

public class UserModel
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Biography { get; set; }

    public int? AddressId { get; set; }
    public AddressEntity? Address { get; set; }
}
