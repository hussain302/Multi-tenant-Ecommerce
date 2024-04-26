
using System.Security.Cryptography;

namespace Application.Services.Cryptography;

[Obsolete]
public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        byte[] salt = new byte[16];
        using (RNGCryptoServiceProvider rng = new())
        {
            rng.GetBytes(salt);
        }

        byte[] hash = new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        string hashedPassword = Convert.ToBase64String(hashBytes);

        return hashedPassword;
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        byte[] hash = new byte[20];
        Array.Copy(hashBytes, 16, hash, 0, 20);

        byte[] computedHash = new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
            if (hash[i] != computedHash[i])
            {
                return false;
            }
        }
        return true;
    }
}