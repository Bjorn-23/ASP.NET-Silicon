using Business.Factories;
using Business.Models;
using Infrastructure.Context;
using Infrastructure.Entitites;
using Infrastructure.Factories;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Xml;

namespace Business.Services;

public class UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signIn, ApplicationDbContext context)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signIn = signIn;
    private readonly ApplicationDbContext _context = context;


    public async Task<ResponseResult> RegisterUserAsync(SignUpModel model)
    {
        try
        {
            var exists = await _userManager.Users.AnyAsync(User => User.Email == model.Email);
            if (!exists)
            {
                UserEntity entity = UserFactory.Create(model);

                var result = await _userManager.CreateAsync(entity, model.Password);
                if (result != null)
                {
                    return ResponseFactory.Ok(result, "user created succesfully.");
                }
            }

            return ResponseFactory.Exists("A user with that email alredy exists.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message + "CreateUserAsync");
        }
    }


    public async Task<ResponseResult> SignInUserAsync(SignInModel user)
    {
        try
        {
            var existingUser = await _userManager.Users.AnyAsync(x => x.Email == user.Email);
            if (existingUser)
            {
                var result = await _signIn.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
                if (result.Succeeded)
                    return ResponseFactory.Ok("User succesfully signed in");
            }

            return ResponseFactory.NotFound("User or password incorrect");

        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "SignInUserAsync"); }
    }


    public async Task<ResponseResult> GetActiveUserAsync(ClaimsPrincipal User)
    {
        try
        {
            var userEntity = await _userManager.GetUserAsync(User);
            if (userEntity != null)
                return ResponseFactory.Ok(UserFactory.Create(userEntity));

            return ResponseFactory.NotFound("No active user could be found");
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "GetActiveUserAsync"); }

    }

    public async Task<ResponseResult> UpdateUserAsync(ClaimsPrincipal User, AccountDetailsBasicInfoModel model)
    {
        try
        {
            var userEntity = await _userManager.GetUserAsync(User);
            if (userEntity != null)
            {
                userEntity.FirstName = model.FirstName ?? userEntity.FirstName;
                userEntity.LastName = model.LastName ?? userEntity.LastName;
                userEntity.Email = model.Email ?? userEntity.Email;
                userEntity.PhoneNumber = model.Phone;
                userEntity.Biography = model.Biography;                

                var result = await _userManager.UpdateAsync(userEntity);
                if (result != null)
                    return ResponseFactory.Ok(UserFactory.Create(userEntity));
            }

            return ResponseFactory.NotFound("No active user could be found");
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "UpdateUserAsync"); }
    }

    public async Task<ResponseResult> CreateOrUpdateAddressAsync(ClaimsPrincipal User, AccountDetailsAddressInfoModel model)
    {
        try
        {
            AddressEntity addressEntity = new AddressEntity()
            {
                StreetName_1 = model.AddresLine_1,
                StreetName_2 = model.AddressLine_2,
                PostalCode = model.PostalCode,
                City = model.City,
            };

            var exists = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName_1 == addressEntity.StreetName_1 && x.StreetName_2 == addressEntity.StreetName_2 && x.PostalCode == addressEntity.PostalCode && x.City == addressEntity.City);
            if (exists == null)
            {
                var newAddress = await _context.Addresses.AddAsync(addressEntity);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        user.AddressId = newAddress.Entity.Id;
                        var updateUser = await _userManager.UpdateAsync(user);
                        if (updateUser != null)
                            return ResponseFactory.Ok("Address created succefully");
                    }
                }
            }
            else
            {
                addressEntity.Id = exists.Id;
                _context.Addresses.Entry(exists).CurrentValues.SetValues(addressEntity);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return ResponseFactory.Ok("Address updated succefully");
                }
            }

            return ResponseFactory.Error("CreateOrUpdateAddressAsync");
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "CreateOrUpdateAddressAsync"); }
    }

    public async Task<ResponseResult> GetUserAddressAsync(ClaimsPrincipal User)
    {
        try
        {
            var userEntity = await _userManager.GetUserAsync(User);
            if (userEntity != null)
            {
                var result = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == userEntity.AddressId);
                if (result != null)
                    return ResponseFactory.Ok(AddressFactory.Create(result));
            }

            return ResponseFactory.NotFound("No active user could be found");
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "GetActiveUserAsync"); }

    }
}
