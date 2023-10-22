using Blazored.LocalStorage;
using Blazored.Toast;
using Pw_Frontend.Application.Services;
using PW_Frontend.Application.Services;
using PW_Frontend.Helpers;

namespace Pw_Frontend.Application.Extension;

public static class ServicesAndRepositoryExtension
{
    public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
    {
        #region Repository



        #endregion
        #region Service

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IHttpService, HttpService>();
        services.AddTransient<CryptoInteropService>();
        services.AddTransient<EncryptionHelper>();

        #endregion

        services.AddBlazoredLocalStorage();
        services.AddBlazoredToast();

        return services;
    }
}