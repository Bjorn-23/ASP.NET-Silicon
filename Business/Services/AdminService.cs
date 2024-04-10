using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

using Business.Models;
using Business.Factories;
using Infrastructure.Context;
using Infrastructure.Entitites;
using Infrastructure.Factories;
using Infrastructure.Utilities;

namespace Business.Services;

public class AdminService(UserManager<UserEntity> userManager, ApplicationDbContext dbContext)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ApplicationDbContext _dbContext = dbContext;

    #region Admin Search
    public async Task<Object> SearchDataByStringInput(AdminSearchModel search)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(search.Expression))
            {
                var query = search.Expression.ToLower();

                switch (query)
                {
                    case "users":
                        var users = await GetAllUsersAsync();
                        if (users.Count() == 1)
                        {
                            var user = users.FirstOrDefault();
                            if (user != null)
                                return user;
                        }

                        return (users);
 
                    case "addresses":
                        var addresses = await GetAllAddressesAsync();
                        return (addresses);                        

                    default:
                        var result = await GetEntitiesByExpression(search.Expression);
                        return result;
                        
                }
            }
            return (null!);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return (null!);
    }

    private async Task<Object> GetEntitiesByExpression(string search)
    {
        try
        {
            if (search != null)
            {
                var emailUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == search);
                if (emailUser != null)
                    return UserFactory.Create(emailUser);

                //string[] searchArray = search.Split(',');
                string[] searchArray = search.Split(", ");

                if (searchArray.Length == 2)
                {
                    var users = await _dbContext.Users.Where(x => x.FirstName == searchArray[0] && x.LastName == searchArray[1]).ToListAsync();
                    if (users.Count() > 1)
                    {
                        List<BasicInfoModel> userModels = [];

                        foreach (var user in users)
                        {
                            userModels.Add(UserFactory.Create(user));
                        }

                        return userModels;
                    }
                    else
                    {
                        if (users.Count() == 1)
                        {
                            BasicInfoModel user = UserFactory.Create(users.FirstOrDefault()!);
                            return user;

                        }
                    }

                    var addresses = await _dbContext.Addresses.Where(x => x.StreetName_1 == searchArray[0] && x.PostalCode == searchArray[1]).ToListAsync();
                    if ( addresses.Count() > 1)
                    {
                        List<AddressInfoModel> addressModels = [];

                        foreach (var address in addresses)
                        {
                            addressModels.Add(AddressFactory.Create(address));
                        }
                                                
                        return addressModels;
                    }
                    else
                    {
                        if (addresses.Count() == 1)
                        {
                            AddressInfoModel address = AddressFactory.Create(addresses.FirstOrDefault()!);
                            return address;

                        }
                    }
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    private async Task<IEnumerable<BasicInfoModel>> GetAllUsersAsync()
    {
        try
        {
            var userEntities = await _userManager.Users.ToListAsync();

            List<BasicInfoModel> basicInfoModels = [];

            if (userEntities.Count >= 1)
            {
                foreach (var entity in userEntities)
                {
                    basicInfoModels.Add(UserFactory.Create(entity));
                }
                return basicInfoModels;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    private async Task<IEnumerable<AddressInfoModel>> GetAllAddressesAsync()
    {
        try
        {
            var addressEntities = await _dbContext.Addresses.ToListAsync();
            List<AddressInfoModel> addressInfoModels = [];

            if (addressEntities != null)
            {
                foreach (var entity in addressEntities)
                {
                    addressInfoModels.Add(AddressFactory.Create(entity));
                }
                return addressInfoModels;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
    #endregion

    public async Task<ResponseResult> UpdateUserAsync(BasicInfoModel model)
    {
        try
        {
            var userEntity = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (userEntity != null)
            {
                userEntity.FirstName = model.FirstName ?? userEntity.FirstName;
                userEntity.LastName = model.LastName ?? userEntity.LastName;
                userEntity.Email = model.Email ?? userEntity.Email;
                userEntity.PhoneNumber = model.Phone ?? userEntity.PhoneNumber;
                userEntity.Biography = model.Biography ?? userEntity.Biography;

                var updatedUser = await _userManager.UpdateAsync(userEntity);
                if (updatedUser.Succeeded)
                {
                    return ResponseFactory.Ok(UserFactory.Create(userEntity));
                }
            }

            return ResponseFactory.NotFound("Could not find a user to update");
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message + "UpdateUserAsyncAsAdmin"); }
        return ResponseFactory.BadRequest();
    }

    public async Task<ResponseResult> UpdateUserPasswordAsync(UserEntity userEntity, PasswordUpdateModel model)
    {
        try
        {
            if (userEntity != null)
            {
                if (userEntity.IsExternalAccount)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(userEntity);
                    var resetResult = await _userManager.ResetPasswordAsync(userEntity, token, model.NewPassword);
                    if (resetResult.Succeeded)
                    {
                        userEntity.IsExternalAccount = false;
                        var userResult = await _userManager.UpdateAsync(userEntity);
                        if (userResult.Succeeded)
                            return ResponseFactory.Ok(UserFactory.Create(userEntity), "Password updated successfully");
                    }
                }
                else
                {
                    var result = await _userManager.ChangePasswordAsync(userEntity, model.CurrentPassword, model.NewPassword);
                    if (result.Succeeded)
                        return ResponseFactory.Ok(UserFactory.Create(userEntity), "Password updated successfully");

                    else
                        return ResponseFactory.BadRequest("Failed to update password");
                }
            }

            //Possible unnecessary as method can only be reacched by logged in user? Maybe for admin??
            return ResponseFactory.NotFound("No active user could be found");
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message + "UpdateUserPasswordAsync"); }
        return ResponseFactory.BadRequest();
    }

    public async Task<ResponseResult> GetOneUserByIdAsync(string id)
    {
        try
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
                return ResponseFactory.Ok(result);

            else
                return ResponseFactory.NotFound("ex.Message");
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message + "GetOneUserByIdAsync"); }
        return ResponseFactory.BadRequest();
    }

}
