using Business.Models;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Security.Claims;

namespace Business.Factories;

public class UserFactory
{
    public static UserEntity Create(SignUpModel model)
    {
        try
        {
            return new UserEntity
            {                
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                ProfileImageUrl = model.ProfileImageUrl,                
            };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public static BasicInfoModel Create(UserEntity entity)
    {
        try
        {
            return new BasicInfoModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email!,
                Phone = entity.PhoneNumber,
                Biography = entity.Biography,
                IsExternalAccount = entity.IsExternalAccount,
                ProfileImageUrl = entity.ProfileImageUrl,
                SavedCourses = entity.SavedCourses ?? [],
            };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public static UserEntity Create(BasicInfoModel model)
    {
        try
        {
            return new UserEntity
            {
                Id = model.Id!,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email!,
                PhoneNumber = model.Phone,
                Biography = model.Biography,
                IsExternalAccount = model.IsExternalAccount,
                ProfileImageUrl = model.ProfileImageUrl,
                SavedCourses = model.SavedCourses ?? [],
            };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public static UserEntity Create(ExternalLoginInfo info)
    {
        try
        {
            return new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true,
                ProfileImageUrl = "avatar.png",
            };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
