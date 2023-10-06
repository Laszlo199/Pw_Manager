using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Pw_Security.Helper;

public class AuthHelper: IAuthHelper
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        // Generate a random salt (16 bytes recommended for Argon2id)
        passwordSalt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(passwordSalt);
        }

// Initialize the Argon2id hasher
        using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            argon2.Salt = passwordSalt;

            // Perform the hashing
            argon2.DegreeOfParallelism = 8; // four cores
            argon2.Iterations = 4;
            passwordHash = argon2.GetBytes(64); // 64 bytes
        }
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        // Initialize the Argon2id verifier
        using (var verifier = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            verifier.Salt = storedSalt;

            // Perform the verification
            byte[] computedHashBytes = verifier.GetBytes(64); // 64 bytes is the size of the stored hash

            // Compare the computed hash with the stored hash
            for (int i = 0; i < computedHashBytes.Length; i++)
            {
                if (computedHashBytes[i] != storedHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}