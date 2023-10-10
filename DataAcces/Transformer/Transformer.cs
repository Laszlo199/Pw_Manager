using System.Security.Cryptography;
using System.Text;

namespace DataAcces.Transformer;

public class Transformer: ITransformer
{
    private const int KeySize = 256; // AES-256

    public string EncryptPassword(string password)
    {
        byte[] encryptionKey = Generate256BitAesKey();

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = encryptionKey;
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(password);
                    }
                }

                byte[] iv = aesAlg.IV;
                byte[] encryptedBytes = msEncrypt.ToArray();
                byte[] result = new byte[iv.Length + encryptedBytes.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);

                return Convert.ToBase64String(result);
            }
        }
    }

    public string DecryptPassword(string encryptedPassword)
    {
        byte[] encryptionKey = Generate256BitAesKey();
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = encryptionKey;

            byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
            byte[] iv = new byte[aesAlg.BlockSize / 8];
            byte[] cipherText = new byte[encryptedBytes.Length - iv.Length];

            Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length);

            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
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
    
    public static byte[] Generate256BitAesKey()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = 256; // Set the desired key size
            aesAlg.GenerateKey();
            return aesAlg.Key;
        }
    }
}