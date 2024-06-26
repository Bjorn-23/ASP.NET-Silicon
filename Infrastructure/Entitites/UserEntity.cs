﻿using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entitites;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    public string? Biography { get; set; }

    public int? AddressId { get; set; }

    public AddressEntity? Address { get; set; }

    public bool IsExternalAccount { get; set; } = false;

    public string ProfileImageUrl { get; set; } = null!;

    public ICollection<SavedCoursesEntity>? SavedCourses { get; set; }
}
