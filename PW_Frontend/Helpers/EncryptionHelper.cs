using System.Security.Cryptography;
using System.Text;
namespace PW_Frontend.Helpers {
    public class EncryptionHelper {
        
        /// <summary>
        /// Generate a master key used to derive the Master password hash for authentication, as well as the stretched master key for encrypting vault data
        /// </summary>
        /// <param name="email">User's Email</param>
        /// <param name="masterPass">User's master password</param>
        /// <returns>Master key as a Byte array</returns>
        public static byte[] GenerateMasterKey(string email, string masterPass) {

            const int iterations = 600000;
            const int keyLength = 32; // Key length in bytes (32 * 8 = 256 bits)
            byte[] emailSalt = Encoding.UTF8.GetBytes(email);
            
            // Create an instance of Rfc2898DeriveBytes
            using Rfc2898DeriveBytes pbkdf2 = new(masterPass, emailSalt, iterations,HashAlgorithmName.SHA256);
            byte[] derivedKey = pbkdf2.GetBytes(keyLength);

            return derivedKey;
        }

        /// <summary>
        /// Generate the Master password hash for authentication
        /// </summary>
        /// <param name="masterPass">User's master password to be used as salt</param>
        /// <param name="masterKey">User's master key</param>
        /// <returns></returns>
        public static byte[] GenerateMasterHash(string masterPass, byte[] masterKey) {
            const int iterations = 100000;
            const int keyLength = 32; // Key length in bytes (32 * 8 = 256 bits)
            byte[] masterSalt = Encoding.UTF8.GetBytes(masterPass);
            
            // Create an instance of Rfc2898DeriveBytes
            using Rfc2898DeriveBytes pbkdf2 = new(masterPass, masterSalt, iterations,HashAlgorithmName.SHA256);
            byte[] derivedKey = pbkdf2.GetBytes(keyLength);

            return derivedKey;
        }

        /// <summary>
        /// Stretch a master key using HMAC from 256bit to 512bit
        /// </summary>
        /// <param name="masterKey">User's master key</param>
        /// <returns></returns>
        public static byte[] GenerateStretchedMasterKey(byte[] masterKey) {
            using HMACSHA256 hmac = new();
            // Initialize the HMAC with the input key
            hmac.Key = masterKey;
    
            // Perform the HKDF extraction step
            byte[] prk = hmac.ComputeHash(masterKey);
    
            // Initialize the HMAC with the extracted PRK
            hmac.Key = prk;
                
            byte[] info = Array.Empty<byte>();
            int length = 64;    // Desired output length in bytes (64 * 8 = 512 bits)

            // Calculate the expanded key using the HKDF expansion step
            byte[] outputKey = new byte[length];
            byte[] t = Array.Empty<byte>();
            int iterations = (int)Math.Ceiling((double)length / hmac.HashSize);

            for (int i = 1; i <= iterations; i++)
            {
                t = hmac.ComputeHash(ConcatenateBytes(t, info, BitConverter.GetBytes(i)));
                Array.Copy(t, 0, outputKey, (i - 1) * hmac.HashSize, Math.Min(hmac.HashSize, length - (i - 1) * hmac.HashSize));
            }

            return outputKey;
        }
        
        
        // TODO: this won't work in browser (sadge) we need to find a workaround
        public string EncryptPassword(string password, byte[] key) {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
                
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new())
            {
                using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);
                    csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                    csEncrypt.FlushFinalBlock();

                    byte[] ivAndEncryptedBytes = new byte[aesAlg.IV.Length + msEncrypt.Length];
                    Array.Copy(aesAlg.IV, ivAndEncryptedBytes, aesAlg.IV.Length);
                    Array.Copy(msEncrypt.ToArray(), 0, ivAndEncryptedBytes, aesAlg.IV.Length, msEncrypt.Length);

                    return Convert.ToBase64String(ivAndEncryptedBytes);
                }
            }
        }

        
        // TODO: this won't work in browser (sadge) we need to find a workaround
        public string DecryptPassword(string encryptedPassword, byte[] key)
        {
            byte[] ivAndEncryptedBytes = Convert.FromBase64String(encryptedPassword);

            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = key;
                aesAlg.IV = ivAndEncryptedBytes.Take(aesAlg.IV.Length).ToArray();

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new(ivAndEncryptedBytes, aesAlg.IV.Length, ivAndEncryptedBytes.Length - aesAlg.IV.Length))
                {
                    using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static byte[] ConcatenateBytes(params byte[][] arrays)
        {
            int totalLength = arrays.Sum(arr => arr.Length);
            byte[] result = new byte[totalLength];
            int offset = 0;

            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, result, offset, array.Length);
                offset += array.Length;
            }

            return result;
        }
    }
}
