using Business.Models;
using Infrastructure.Entitites;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Business.Factories;

public class AddressFactory
{
    public static AddressEntity Create(AccountDetailsAddressInfoModel model)
    {
        try
        {
            var address = new AddressEntity()
            {
                Id = model.Id,
                StreetName_1 = model.AddresLine_1,
                StreetName_2 = model.AddressLine_2,
                PostalCode = model.PostalCode,
                City = model.City
            };
            return address;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    public static AccountDetailsAddressInfoModel Create(AddressEntity entity)
    {
        try
        {
            var address = new AccountDetailsAddressInfoModel()
            {
                Id = entity.Id,
                AddresLine_1 = entity.StreetName_1,
                AddressLine_2 = entity.StreetName_2,
                PostalCode = entity.PostalCode,
                City = entity.City
            };
            return address;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
}
