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
            UserEntity entity = new()
            {                
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };

            return entity;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message + "failed to create UserEntity in factory from SignUpModel");
            return null!;
        }
    }

    public static BasicInfoModel Create(UserEntity entity)
    {
        try
        {
            BasicInfoModel model = new()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email!,
                Phone = entity.PhoneNumber,
                Biography = entity.Biography,
                IsExternalAccount = entity.IsExternalAccount
            };

            return model;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message + "failed to create UserModel in factory from UserEntity");
            return null!;
        }
    }

    public static UserEntity Create(BasicInfoModel model)
    {
        try
        {
            UserEntity entity = new()
            {
                Id = model.Id!,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email!,
                PhoneNumber = model.Phone,
                Biography = model.Biography,
                IsExternalAccount = model.IsExternalAccount
            };

            return entity;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message + "failed to create UserEntity in factory from UserModel");
            return null!;
        }
    }

    public static UserEntity Create(ExternalLoginInfo info)
    {
        try
        {
            UserEntity entity = new()
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true
            };

            return entity;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message + "failed to create External user in factory from ExternalLoginInfo");
            return null!;
        }
    }
}
