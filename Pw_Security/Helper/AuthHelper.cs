namespace Pw_Security.Helper;

public class AuthHelper: IAuthHelper
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        throw new NotImplementedException();
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        throw new NotImplementedException();
    }
}