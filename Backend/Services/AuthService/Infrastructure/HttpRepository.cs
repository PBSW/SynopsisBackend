using System.Net;
using Application.Interfaces;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Shared.DTOs.Create;
using Shared.DTOs.Response;

namespace Infrastructure;

public class HttpRepository : IHttpRepository
{
    private readonly string _baseUrl;

    public HttpRepository(IOptions<HttpConnectionOptions> baseUrl)
    {
        _baseUrl = baseUrl.Value.UserServiceUrl;
    }

    public async Task<UserResponse> CreateUser(UserCreate userCreate)
    {
        var client = new RestClient(_baseUrl);
        var request = new RestRequest("/api/user", Method.Post);
        request.AddHeader("Content-Type", "application/json");

        string jsonBody = JsonConvert.SerializeObject(userCreate);
        request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(response.Content);

            if (userResponse != null)
            {
                return userResponse;
            }

            throw new ArgumentException("User response is null");
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            // Parse and throw the error message from the response content
            var errorResponse = JsonConvert.DeserializeObject<string>(response.Content);
            throw new Exception($"{errorResponse}");
        }

        // Return false if the HTTP request failed or the user creation was not successful
        throw new Exception("Unable to connect to UserService: " + response.ErrorMessage + response.ErrorException);
    }
}