using Microsoft.Extensions.Configuration;
using Pw_Security.Helper;
using Pw_Security.IRepository;
using Pw_Security.IServices;
using Pw_Security.Models;

namespace Pw_Security.Services;

public class SecurityService: ISecurityService
{
    private readonly IUserRepository _repo;
    private readonly IAuthHelper _authenticationHelper;
    
    public SecurityService(IConfiguration configuration, 
        IUserRepository repository,  IAuthHelper authenticationHelper)
    {
        Configuration = configuration;
        _repo = repository;
        _authenticationHelper = authenticationHelper;
    }
    
    public IConfiguration Configuration { get; }
    
    public JwtToken GenerateJwtToken(string email, string password, out int userId)
    {
        throw new NotImplementedException();
    }

    public bool Create(string loginDtoEmail, string loginDtoPassword)
    {
        throw new NotImplementedException();
    }

    public bool EmailExists(string loginDtoEmail)
    {
        throw new NotImplementedException();
    }
}