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

    public async Task Register(LoginDto loginDto)
    {
        byte[] masterKey = EncryptionHelper.GenerateMasterKey(loginDto.Email, loginDto.Password);
        var masterHash = EncryptionHelper.GenerateMasterHash(loginDto.Password, masterKey);
        loginDto.Password = System.Text.Encoding.UTF8.GetString(masterHash);
        await _httpService.Post("/api/Auth/Register", loginDto);
        loginDto.Password = string.Empty;
    }
    public async Task Login(LoginDto loginDto) {
        byte[] masterKey = EncryptionHelper.GenerateMasterKey(loginDto.Email, loginDto.Password);
        var masterHash = EncryptionHelper.GenerateMasterHash(loginDto.Password, masterKey);
        loginDto.Password = System.Text.Encoding.UTF8.GetString(masterHash);
        TokenDto = await _httpService.Post<TokenDto>("/api/Auth/login", loginDto);
        loginDto.Password = string.Empty;
        byte[] stretchedKey = EncryptionHelper.GenerateStretchedMasterKey(masterKey);
        await _localStorageService.SetItemAsync("stretchedKey", stretchedKey);
        await _localStorageService.SetItemAsync("tokenDto", TokenDto);
    }

    public async Task Logout()
    {
        TokenDto = null;
        await _localStorageService.RemoveItemAsync("tokenDto");
        await _localStorageService.RemoveItemAsync("stretchedKey");
        _navigationManager.NavigateTo("login");
    }
}