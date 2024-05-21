using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class HttpRepository : IHttpRepository
{
    private readonly string _baseUrl;
    
    public HttpRepository(IOptions<ConnectionOptions> baseUrl)
    {
        _baseUrl = baseUrl.Value.UserServiceUrl;
    }
    public Task<bool> IsUser(int id)
    {
        throw new NotImplementedException();
    }
}