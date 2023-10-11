using System.Security.Cryptography;
using System.Text;

namespace DataAcces.Transformer;

public class Transformer: ITransformer
{
    public string EncryptPassword(string password)
    {
        string key = "asd";
        using (Aes aesAlg = Aes.Create())
        {
            var properSizeString = Generate128BitString(key);
            aesAlg.Key = Encoding.UTF8.GetBytes(properSizeString);
            
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
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
    }

    public string DecryptPassword(string encryptedPassword)
    {
        string key = "asd";
        byte[] ivAndEncryptedBytes = Convert.FromBase64String(encryptedPassword);

        using (Aes aesAlg = Aes.Create())
        {
            var properSizeString = Generate128BitString(key);
            aesAlg.Key = Encoding.UTF8.GetBytes(properSizeString);
            aesAlg.IV = ivAndEncryptedBytes.Take(aesAlg.IV.Length).ToArray();

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(ivAndEncryptedBytes, aesAlg.IV.Length, ivAndEncryptedBytes.Length - aesAlg.IV.Length))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    public static string Generate128BitString(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            byte[] first16Bytes = new byte[16];
            Array.Copy(hash, 0, first16Bytes, 0, 16);
            return BitConverter.ToString(first16Bytes).Replace("-", "");
        }
    }
}