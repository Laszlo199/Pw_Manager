using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Pw_Frontend.Dtos.AuthDto;

namespace Pw_Frontend.Application.Services;

public interface IAuthenticationService
{
    TokenDto? TokenDto { get; }
    Task Initialize();
    Task Register(LoginDto loginDto);
    Task Login(LoginDto loginDto);
    Task Logout();
}

public class AuthenticationService : IAuthenticationService
{
    private IHttpService _httpService;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;
    public TokenDto? TokenDto { get; private set; }

    public AuthenticationService(
        IHttpService httpService,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService
    )
    {
        _httpService = httpService;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    public async Task Initialize()
    {
        TokenDto = await _localStorageService.GetItemAsync<TokenDto>("tokenDto");
    }

    public async Task Register(LoginDto loginDto)
    {
        await _httpService.Post("/api/Auth/Register", loginDto);
    }
    public async Task Login(LoginDto loginDto)
    {
        TokenDto = await _httpService.Post<TokenDto>("/api/Auth/login", loginDto);
        await _localStorageService.SetItemAsync("tokenDto", TokenDto);
    }

    public async Task Logout()
    {
        TokenDto = null;
        await _localStorageService.RemoveItemAsync("tokenDto");
        _navigationManager.NavigateTo("login");
    }
}