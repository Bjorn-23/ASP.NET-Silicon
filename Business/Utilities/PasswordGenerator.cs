using System.Security.Cryptography;
using System.Text;

namespace Business.Utilities;

internal class PasswordGenerator
{
    internal static (string Password, string SecurityKey) GenerateSecurePassword(string password)
    {
        using var hmac = new HMACSHA512();

        var securityKey = Convert.ToBase64String(hmac.Key);

        var generatedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

        return (generatedPassword, securityKey);
    }


    internal static bool VerifyPassword(string password, string storedSecurityKey, string StoredPassword)
    {
        byte[] keyBytes = Convert.FromBase64String(storedSecurityKey);

        using (var hmac = new HMACSHA512(keyBytes))
        {
            var computedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

            return ConstantTimeEquals(StoredPassword, computedPassword);
        }
    }

    private protected static bool ConstantTimeEquals(string storedPassword, string computedPassword)
    {
        if (storedPassword == null || computedPassword == null)
        {
            return false;
        }

        // Convert strings to byte arrays using UTF-8 encoding
        byte[] aBytes = Encoding.UTF8.GetBytes(storedPassword);
        byte[] bBytes = Encoding.UTF8.GetBytes(computedPassword);

        // If the lengths are not equal, return false immediately
        if (aBytes.Length != bBytes.Length)
        {
            return false;
        }

        // Compare each byte of the arrays
        int result = 0;
        for (int i = 0; i < aBytes.Length; i++)
        {
            result |= aBytes[i] ^ bBytes[i];
        }

        // If the result is zero, the strings are equal; otherwise, they are not
        return result == 0;
    }

}

