using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using PW_Frontend.Application.Dtos.AuthDto;
using Pw_Frontend.Dtos.AuthDto;
using PW_Frontend.Helpers;

namespace Pw_Frontend.Application.Services;

public interface IAuthenticationService
{
    TokenDto? TokenDto { get; }
    byte[]? StretchedKey { get; }
    Task Initialize();
    Task<bool> Register(LoginDto loginDto);
    Task<bool> Login(LoginDto loginDto);
    Task Logout();
}

public class AuthenticationService : IAuthenticationService
{
    private IHttpService _httpService;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;
    public TokenDto? TokenDto { get; private set; }
    public byte[]? StretchedKey { get; private set; }

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
        StretchedKey = await _localStorageService.GetItemAsync<byte[]>("stretchedKey");
    }

    public async Task<bool> Register(LoginDto loginDto)
    {
        
        
        byte[] masterKey = EncryptionHelper.GenerateMasterKey(loginDto.Email, loginDto.Password);
        var masterHash = EncryptionHelper.GenerateMasterHash(loginDto.Password, masterKey);
        var loginDtoCopy = new LoginDto {
            Email = loginDto.Email,
            Password = System.Text.Encoding.UTF8.GetString(masterHash)
        };
        TokenDto = await _httpService.Post<TokenDto>("/api/Auth/Register", loginDtoCopy);
        if (TokenDto is null) {
            return false;
        }
        await _localStorageService.SetItemAsync("masterKey", masterKey);
        await _localStorageService.SetItemAsync("tokenDto", TokenDto);
        return true;
    }
    
    public async Task<bool> Login(LoginDto loginDto) {
        
        byte[] masterKey = EncryptionHelper.GenerateMasterKey(loginDto.Email, loginDto.Password);
        var masterHash = EncryptionHelper.GenerateMasterHash(loginDto.Password, masterKey);
        var loginDtoCopy = new LoginDto {
            Email = loginDto.Email,
            Password = System.Text.Encoding.UTF8.GetString(masterHash)
        };
        TokenDto = await _httpService.Post<TokenDto>("/api/Auth/login", loginDtoCopy);
        if (TokenDto is null) {
            return false;
        }
        await _localStorageService.SetItemAsync("masterKey", masterKey);
        await _localStorageService.SetItemAsync("tokenDto", TokenDto);
        return true;
    }

    public async Task Logout()
    {
        TokenDto = null;
        await _localStorageService.RemoveItemAsync("tokenDto");
        await _localStorageService.RemoveItemAsync("masterKey");
        _navigationManager.NavigateTo("login");
    }
}