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
            argon2.Iterations = 4;
            // hashing
            argon2.DegreeOfParallelism = 8; // cores
            
            argon2.MemorySize = 65536; //Have to be bigger than 4kB lol
            passwordHash = argon2.GetBytes(64); 
        }
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        try
        {
            // Argon2id verifier
            using (var verifier = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                verifier.Salt = storedSalt;

                // Perform the verification
                byte[] computedHashBytes = verifier.GetBytes(64); // 64 bytes is the size of the stored hash

                // Use constant-time comparison to compare hashes
                return ConstantTimeAreEqual(computedHashBytes, storedHash);
            }
        }
        catch (Exception)
        {
            // Handle exceptions, such as invalid Argon2 parameters or salt
            return false;
        }
    }

// Constant-time comparison function
    private bool ConstantTimeAreEqual(byte[] a, byte[] b)
    {
        if (a == null || b == null || a.Length != b.Length)
        {
            return false;
        }

        bool areEqual = true;

        for (int i = 0; i < a.Length; i++)
        {
            areEqual &= (a[i] == b[i]);
        }

        return areEqual;
    }
}