using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using Blazored.Toast.Configuration;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Pw_Frontend.Application.Dtos;
using Pw_Frontend.Dtos.AuthDto;
using PW_Frontend.Shared;

namespace Pw_Frontend.Application.Services;

public interface IHttpService
{
    Task<T?> Get<T>(string uri);
    Task<T?> Post<T>(string uri, object? body = null);
    Task Post(string uri, object? body = null);
    Task<T?> Put<T>(string uri, object? body = null);
    Task Put(string uri, object? body = null);
    Task<T?> Delete<T>(string uri, object? body = null);
    Task Delete(string uri, object? body = null);
}

public class HttpService : IHttpService
{
    private HttpClient _httpClient;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;
    private IConfiguration _configuration;
    private IToastService _toastService;

    public HttpService(
        HttpClient httpClient,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService,
        IConfiguration configuration,
        IToastService toastService)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        _configuration = configuration;
        _toastService = toastService;
    }

    public async Task<T?> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }

    public async Task<T?> Post<T>(string uri, object? body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        if (body != null)
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        return await SendRequest<T>(request);
    }
    public async Task Post(string uri, object? body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        if (body != null) {
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        }
        await SendRequest(request);
    }

    public async Task<T?> Put<T>(string uri, object? body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        if (body != null)
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        return await SendRequest<T>(request);
    }
    public async Task Put(string uri, object? body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        if (body != null)
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        await SendRequest(request);
    }

    public async Task<T?> Delete<T>(string uri, object? body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        if (body != null)
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        return await SendRequest<T>(request);
    }
    public async Task Delete(string uri, object? body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        if (body != null)
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        await SendRequest(request);
    }

    // helper methods

    private async Task<T?> SendRequest<T>(HttpRequestMessage request)
    {
        // add jwt auth header if user is logged in and request is to the api url
        var token = await _localStorageService.GetItemAsync<TokenDto>("tokenDto");
        var isApiUrl = !request.RequestUri?.IsAbsoluteUri;
        if (token != null && isApiUrl != null && isApiUrl.Value)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Jwt);

        using var response = await _httpClient.SendAsync(request);

        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("logout");
            return default!;
        }

        // 403 response
        if (response.StatusCode == HttpStatusCode.Forbidden) {
            _toastService.ShowError("You do not have permission to access this resource.");
            return default!;
        }

        // throw exception on error response
        if (!response.IsSuccessStatusCode) {
            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponseDto>();
            _toastService.ShowError(errorResponse is not null ? errorResponse.Error : "Unknown Exception occured!");
            return default!;
        }
        return await response.Content.ReadFromJsonAsync<T>();
    }
    private async Task SendRequest(HttpRequestMessage request)
    {
        // add jwt auth header if user is logged in and request is to the api url
        var token = await _localStorageService.GetItemAsync<TokenDto>("tokenDto");
        var isApiUrl = !request.RequestUri?.IsAbsoluteUri;
        if (token != null && isApiUrl != null && isApiUrl.Value)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Jwt);

        using var response = await _httpClient.SendAsync(request);

        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("logout");
        }

        // throw exception on error response
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception(error?["message"]);
        }
    }
}