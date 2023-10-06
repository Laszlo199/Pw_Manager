namespace Pw_Security.Helper;

public interface IAuthHelper
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
      
    bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
}