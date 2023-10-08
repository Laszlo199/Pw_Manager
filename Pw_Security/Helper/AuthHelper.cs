using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Pw_Security.Helper;

public class AuthHelper: IAuthHelper
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        // Generate salt (16 bytes)
        passwordSalt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(passwordSalt);
        }

        // Argon2id hasher
        using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            argon2.Salt = passwordSalt;
            
            // hashing
            argon2.DegreeOfParallelism = 8; // cores
            argon2.Iterations = 4;
            argon2.MemorySize = 65536; //Have to be bigger than 4kB lol
            passwordHash = argon2.GetBytes(64); 
        }
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        try
        {
            // Create an Argon2id verifier
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = storedSalt;
                argon2.DegreeOfParallelism = 8;
                argon2.Iterations = 4;
                argon2.MemorySize = 65536;
                
                byte[] inputPasswordHash = argon2.GetBytes(64);
                
                return storedHash.SequenceEqual(inputPasswordHash);
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}