namespace Application.Helpers;

public interface IPasswordHasher
{
    public Task<string> HashPassword(string password, byte[] salt);
}