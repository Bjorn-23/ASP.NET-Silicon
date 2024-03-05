using Business.Models;
using Infrastructure.Entitites;
using System.Diagnostics;

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

    //public static UserModel Create(UserEntity entity)
    //{
    //    try
    //    {
    //        UserModel model = new()
    //        {
    //            Id = entity.Id,
    //            FirstName = entity.FirstName,
    //            LastName = entity.LastName,
    //            Email = entity.Email!,
    //            Phone = entity.PhoneNumber,
    //            //Biography = entity.Biography,
    //            Address = entity.Address,
    //            AddressId = entity.AddressId,


    //        };

    //        return model;

    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex.Message + "failed to create UserModel in factory from UserEntity");
    //        return null!;
    //    }
    //}

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
                Biography = entity.Biography
            };

            return model;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message + "failed to create UserModel in factory from UserEntity");
            return null!;
        }
    }
}
