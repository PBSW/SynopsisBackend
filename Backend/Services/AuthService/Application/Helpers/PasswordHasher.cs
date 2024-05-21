using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Application.Helpers;

public class PasswordHasher : IPasswordHasher
{
    public Task<string> HashPassword(string password, byte[] salt)
    {
        if (password == null || salt == null)
        {
            throw new ArgumentNullException(nameof(password), nameof(salt));
        }

        return Task.Run(() => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            10000,
            256 / 8)));
    }
}