using Application.Interfaces;
using Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Infrastructure;

public class HttpRepository : IHttpRepository
{
    private readonly string _baseUrl;
    
    public HttpRepository(IOptions<ConnectionOptions> baseUrl)
    {
        _baseUrl = baseUrl.Value.UserServiceUrl ?? throw new ArgumentException("UserServiceUrl is null");
    }
    public async Task<bool> IsUser(int id)
    {
        var client = new RestClient(_baseUrl);
        var request = new RestRequest($"api/user/exists/{id}", Method.Get);
        request.AddHeader("Content-Type", "application/json");
        
        var response = await client.ExecuteAsync<bool>(request);
        
        if (response.IsSuccessful)
        {
            return response.Data;
        }
        
        throw new Exception("Unable to connect to UserService: " + response.ErrorMessage);
    }
}