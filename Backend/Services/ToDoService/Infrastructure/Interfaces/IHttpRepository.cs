namespace Infrastructure.Interfaces;

public interface IHttpRepository
{
    public Task<bool> IsUser(int id);
}