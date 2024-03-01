using Business.Factories;
using Business.Models;
using Business.Utilities;
using Infrastructure.Entitites;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.Utilities;

namespace Business.Services;

public class UserService(UserRepository repository, AddressService addressService)
{
    private readonly UserRepository _repository = repository;
    private readonly AddressService _addressService = addressService;

    public async Task<ResponseResult> CreateUserAsync(SignUpModel user)
    {
        try
        {
            var doesUserExist = await _repository.AlreadyExistsAsync(x => x.Email == user.Email);
            if (doesUserExist.StatusCode == StatusCode.NOT_FOUND)
            {
                var newUser = UserFactory.Create(user);

                var result = await _repository.CreateAsync(newUser);
                if (result.StatusCode == StatusCode.OK)
                {
                    //Since I dont want to send any info back to graphical UI I create an empty instance of the responseResult
                    return ResponseFactory.Ok("User created succesfully.");
                }
            }

            return doesUserExist;

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
            var existingUser = await _repository.GetOneAsync(x => x.Email == user.Email);
            if (existingUser.StatusCode == StatusCode.OK)
            {
                var entity = (UserEntity)existingUser.ContentResult!;
                var result = PasswordGenerator.VerifyPassword(user.Password, entity.SecurityKey, entity.Password);
                if (result)
                    return ResponseFactory.Ok(entity.Id, "User succesfully signed in");
            }

            return ResponseFactory.NotFound();

        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "SiginUserAsync"); }
    }

}
