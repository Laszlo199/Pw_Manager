namespace DataAcces.Transformer;

public interface ITransformer
{
     string EncryptPassword(string password);
     string DecryptPassword(string encryptedPassword);
}