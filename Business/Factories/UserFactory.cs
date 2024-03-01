using Business.Models;
using Business.Utilities;
using Infrastructure.Entitites;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Business.Factories;

public class UserFactory
{
    public static UserEntity Create(SignUpModel user)
    {
        try
        {
            var securityObject = PasswordGenerator.GenerateSecurePassword(user.Password);
            var date = DateTime.Now;

            UserEntity newUser = new()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = securityObject.Password,
                SecurityKey = securityObject.SecurityKey,
                Created = date,
                Modified = date,
            };
            
            return newUser;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message + "Create user in factory from signup");
            return null!;
        }
    }
}
