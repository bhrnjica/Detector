
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Detector.Contract.DTO;
using Detector.Contract.Models;
using Detector.Interfaces;

namespace Detector.App.Services;

public class DetectorClient : IDetectorClient
{
    private readonly HttpClient _client;
    public DetectorClient(HttpClient client) 
    {
        _client = client;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return false;
        }

        var response = await _client.PostAsJsonAsync("auth/login", new UserItem { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateUserAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return false;
        }

        var response = await _client.PostAsJsonAsync("auth/register", new UserItem { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> LogoutAsync()
    {
        var response = await _client.PostAsync("auth/logout", content: null);
        return response.IsSuccessStatusCode;
    }
}