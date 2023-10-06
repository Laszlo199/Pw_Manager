using Pw_Security.Models;

namespace Pw_Security.IServices;

public interface ISecurityService
{
    JwtToken GenerateJwtToken(string email, string password, out int userId);
    bool Create(string loginDtoEmail, string loginDtoPassword);
    bool EmailExists(string loginDtoEmail);
}