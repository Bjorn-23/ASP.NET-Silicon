using Business.Factories;
using Business.Models;
using Infrastructure.Context;
using Infrastructure.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services;

public class AdminService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ApplicationDbContext dbContext)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Object> DetermineDataType(AdminSearchModel search)
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
                        return (users);
 
                    case "addresses":
                        var addresses = await GetAllAddressesAsync();
                        return (addresses);                        

                    default:
                        var result = await ExecuteExpression(search.Expression);
                            return result;
                        
                }
            }
            return (null!);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return (null!);
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


    private async Task<Object> ExecuteExpression(string search)
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
                    if (users.Count() >= 1)
                    {
                        List<BasicInfoModel> userModels = [];

                        foreach (var user in users)
                        {
                            userModels.Add(UserFactory.Create(user));
                        }

                        return userModels;
                    }

                    var addresses = await _dbContext.Addresses.Where(x => x.StreetName_1 == searchArray[0] && x.PostalCode == searchArray[1]).ToListAsync();
                    if ( addresses.Count() >= 1)
                    {
                        List<AddressInfoModel> addressModels = [];

                        foreach (var address in addresses)
                        {
                            addressModels.Add(AddressFactory.Create(address));
                        }

                        return addressModels;
                    }                                      
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
