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

            const int iterations = 6000; // 600k is waay too slow
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
            const int iterations = 1000; // 100k is waay too slow
            const int keyLength = 32; // Key length in bytes (32 * 8 = 256 bits)
            byte[] masterSalt = Encoding.UTF8.GetBytes(masterPass);
            
            // Create an instance of Rfc2898DeriveBytes
            using Rfc2898DeriveBytes pbkdf2 = new(masterPass, masterSalt, iterations,HashAlgorithmName.SHA256);
            byte[] derivedKey = pbkdf2.GetBytes(keyLength);

            return derivedKey;
        }

        //TODO: I tried to stretch the masterKey before using it to encrypt the vault but It keeps throwing argumentErrors related to array length so I gave up for now :P
        /// <summary>
        /// Stretch a master key using HMAC from 256bit to 512bit
        /// </summary>
        /// <param name="masterKey">User's master key</param>
        /// <returns></returns>
        public static byte[] GenerateStretchedMasterKey(byte[] masterKey) {
            using HMACSHA256 hmac = new();

            // Step 1: Extract the PRK (Pseudorandom Key) using the HMAC function
            byte[] prk = hmac.ComputeHash(masterKey);

            // Step 2: Expand the key using the HKDF expansion step
            int length = 64; // Desired output length in bytes (64 * 8 = 512 bits)
            byte[] outputKey = new byte[length];
    
            // The OKM (Output Key Material) should be zero-initialized of the desired length
            byte[] okm = new byte[length];
    
            byte[] info = Array.Empty<byte>();
    
            byte[] t = Array.Empty<byte>();
            for (int i = 1; i <= Math.Ceiling((double)length / hmac.HashSize); i++)
            {
                byte[] prevT = t;

                // Calculate the HMAC input for this iteration
                byte[] input = ConcatenateBytes(prevT, info, BitConverter.GetBytes(i));
        
                // Compute the HMAC for this iteration
                t = hmac.ComputeHash(input);
        
                // Copy the output of this iteration to the OKM
                int copyLength = Math.Min(hmac.HashSize, length - (i - 1) * hmac.HashSize);
                Array.Copy(t, 0, okm, (i - 1) * hmac.HashSize, copyLength);
            }

            return okm;
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
