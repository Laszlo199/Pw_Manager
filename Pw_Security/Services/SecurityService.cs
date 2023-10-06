using Pw_Security.IServices;
using Pw_Security.Models;

namespace Pw_Security.Services;

public class SecurityService: ISecurityService
{
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