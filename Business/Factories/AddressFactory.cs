using Business.Models;
using Infrastructure.Entitites;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Business.Factories;

public class AddressFactory
{
    public static AddressEntity Create(AddressInfoModel model)
    {
        try
        {
            return new AddressEntity()
            {
                Id = model.Id,
                StreetName_1 = model.AddresLine_1,
                StreetName_2 = model.AddressLine_2,
                PostalCode = model.PostalCode,
                City = model.City
            };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public static AddressInfoModel Create(AddressEntity entity)
    {
        try
        {
            return new AddressInfoModel()
            {
                Id = entity.Id,
                AddresLine_1 = entity.StreetName_1,
                AddressLine_2 = entity.StreetName_2,
                PostalCode = entity.PostalCode,
                City = entity.City
            };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
