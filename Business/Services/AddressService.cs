using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Diagnostics;

using Business.Models;
using Business.Factories;
using Infrastructure.Context;
using Infrastructure.Entitites;
using Infrastructure.Factories;
using Infrastructure.Utilities;

namespace Business.Services;

public class AddressService(ApplicationDbContext context, UserManager<UserEntity> userManager)
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<UserEntity> _userManager = userManager;

    /// <summary>
    /// Gets a user address if it exists, else creates it.
    /// </summary>
    /// <param name="User"></param>
    /// <param name="model"></param>
    /// <returns>ResponseResult with address.</returns>
    public async Task<ResponseResult> GetOrCreateAddressAsync(ClaimsPrincipal User, AddressInfoModel model)
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
            if (exists != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.AddressId = exists.Id;
                    var updateUser = await _userManager.UpdateAsync(user);
                    if (updateUser != null)
                        return ResponseFactory.Created("Address added to user succefully");
                }
            }
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
                        {
                            return ResponseFactory.Created("Address created succefully");
                        }
                    }
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ResponseFactory.BadRequest();
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
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ResponseFactory.BadRequest();

    }

    // Theses can be used for admin purposes on addresses
    //public async Task<ResponseResult> GetOrCreateOneAdressAsync(AccountDetailsAddressInfoModel address)
    //{
    //    try
    //    {
    //        var exists = await _repository.GetOneAsync(x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
    //        if (exists.StatusCode == StatusCode.NOT_FOUND)
    //        {
    //            var addressEntity = AddressFactory.Create(address);
    //            var result = await _repository.CreateAsync(addressEntity);
    //            return result;
    //        }

    //        return exists;
    //    }
    //    catch (Exception ex)
    //    {
    //        return ResponseFactory.Error(ex.Message + "GetOneAddressAsync");
    //    }
    //}

    //public async Task<ResponseResult> GetOneAdressAsync(AccountDetailsAddressInfoModel address)
    //{
    //    try
    //    {
    //        var result = await _repository.GetOneAsync(x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return ResponseFactory.Error(ex.Message + "GetOneAddressAsync");
    //    }
    //}

    //public async Task<ResponseResult> CreateAdressAsync(AccountDetailsAddressInfoModel address)
    //{
    //    try
    //    {
    //        var doesAddressExist = await _repository.AlreadyExistsAsync(x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
    //        if (doesAddressExist.StatusCode == StatusCode.NOT_FOUND)
    //        {
    //            var entity = AddressFactory.Create(address);
    //            var result = await _repository.CreateAsync(entity);
    //            return result;
    //        }

    //        return doesAddressExist;

    //    }
    //    catch (Exception ex)
    //    {
    //        return ResponseFactory.Error(ex.Message + "CreateAddressAsync");
    //    }
    //}

    //public async Task<ResponseResult> DeleteAddressAsync(AccountDetailsAddressInfoModel address)
    //{
    //    try
    //    {
    //        var result = await _repository.DeleteAsync(x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return ResponseFactory.Error(ex.Message + "DeleteAddressAsync");
    //    }
    //}

    //public async Task<ResponseResult> UpdateAddressAsync(AccountDetailsAddressInfoModel address, AccountDetailsAddressInfoModel updatedAddress)
    //{
    //    try
    //    {
    //        var updatedEntity = AddressFactory.Create(updatedAddress);
    //        var result = await _repository.UpdateAsync(updatedEntity, x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return ResponseFactory.Error(ex.Message + "DeleteAddressAsync");
    //    }
    //}
}
