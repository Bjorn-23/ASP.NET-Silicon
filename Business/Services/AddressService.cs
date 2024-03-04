//using Business.Factories;
//using Business.Models;
//using Business.Utilities;
//using Infrastructure.Entitites;
//using Infrastructure.Factories;
//using Infrastructure.Repositories;
//using Infrastructure.Utilities;
//using Microsoft.EntityFrameworkCore.Metadata.Conventions;
//using System.Linq.Expressions;
//namespace Business.Services;

//public class AddressService(AddresRepository repository)
//{
//    private readonly AddresRepository _repository = repository;

//    public async Task<ResponseResult> GetOrCreateOneAdressAsync(AccountDetailsAddressInfoModel address)
//    {
//        try
//        {
//            var exists = await _repository.GetOneAsync(x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
//            if (exists.StatusCode == StatusCode.NOT_FOUND)
//            {
//                var addressEntity = AddressFactory.Create(address);
//                var result = await _repository.CreateAsync(addressEntity);
//                return result;
//            }

//            return exists;
//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message + "GetOneAddressAsync");
//        }
//    }

//    public async Task<ResponseResult> GetOneAdressAsync(AccountDetailsAddressInfoModel address)
//    {
//        try
//        {
//            var result = await _repository.GetOneAsync(x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
//            return result;
//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message + "GetOneAddressAsync");
//        }
//    }

//    public async Task<ResponseResult> CreateAdressAsync(AccountDetailsAddressInfoModel address)
//    {
//        try
//        {
//            var doesAddressExist = await _repository.AlreadyExistsAsync(x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
//            if (doesAddressExist.StatusCode == StatusCode.NOT_FOUND)
//            {
//                var entity = AddressFactory.Create(address);
//                var result = await _repository.CreateAsync(entity);
//                return result;
//            }

//            return doesAddressExist;

//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message + "CreateAddressAsync");
//        }
//    }

//    public async Task<ResponseResult> DeleteAddressAsync(AccountDetailsAddressInfoModel address)
//    {
//        try
//        {
//            var result = await _repository.DeleteAsync(x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
//            return result;
//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message + "DeleteAddressAsync");
//        }
//    }

//    public async Task<ResponseResult> UpdateAddressAsync(AccountDetailsAddressInfoModel address, AccountDetailsAddressInfoModel updatedAddress)
//    {
//        try
//        {
//            var updatedEntity = AddressFactory.Create(updatedAddress);
//            var result = await _repository.UpdateAsync(updatedEntity, x => x.StreetName_1 == address.AddresLine_1 && x.PostalCode == address.PostalCode && x.City == address.City);
//            return result;
//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message + "DeleteAddressAsync");
//        }
//    }
//}
