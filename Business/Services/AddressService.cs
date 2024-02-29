using Business.Factories;
using Business.Models;

namespace Business.Services;

public class AddressService(AddressRepository repository)
{
    private readonly AddressRepository _repository = repository;

    public async Task<ReponseResult> CreateAdressAsync(AccountDetailsAddressInfoModel address)
    {
        var entity = AddressFactory.Create(address);
        var result = _repository.CreateOneAsync(entity);
        if (result.Success)
        {
            return result;
        }
    }

}
