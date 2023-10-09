using System.Security.Cryptography;
using Core.Models;
using DataAcces.Entity;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAcces.Repo;

public class ManagerRepository: IManagerRepository
{
    private readonly PwManagerContext _ctx;
    

    public ManagerRepository(PwManagerContext context)
    {
        _ctx = context;
    }
    public List<Passwords> GetAllPasswordsByUserId(int userId)
    {
        //return _ctx.Passwords.Where(p => p.UserEntity.Id == userId).ToList();
        throw new NotImplementedException();
    }

    public Passwords Create(Passwords newPassword)
    {
        var model = new PasswordEntity()
        {
            Id = newPassword.Id,
            UserId = newPassword.User.Id,
            Email = newPassword.Email,
            WebsiteName = newPassword.WebsiteName,
            Password = newPassword.Password,
            DateCreated = DateTime.Today,
            UserEntity = new UserEntity()
            {
                Id = newPassword.User.Id,
                Email = newPassword.User.Email,
                PasswordHash = newPassword.User.PasswordHash,
                PasswordSalt = newPassword.User.PasswordSalt
            }
            
            
        };
        _ctx.Attach(model).State = EntityState.Added;
        _ctx.SaveChanges();
        return newPassword;
    }

    public Passwords Delete(int passwordId)
    {
        throw new NotImplementedException();
    }

    public Passwords Update(Passwords password)
    {
        throw new NotImplementedException();
    }
    
    public string RandomPasswordGenerator(int length)
    {
     const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
     const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
     const string numericChars = "0123456789";
     const string specialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?";
     
     const string allChars = lowercaseChars + uppercaseChars + numericChars + specialChars;
     
        if (length < 7 || length > 20)
        {
            throw new ArgumentOutOfRangeException("length", "Password length must be between 7 and 15 characters.");
        }

        // Create an array to hold the allowed character sets
        string charSet = allChars;

        // Generate a random password using RNGCryptoServiceProvider
        char[] password = new char[length];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);

            for (int i = 0; i < length; i++)
            {
                password[i] = charSet[randomBytes[i] % charSet.Length];
            }
        }

        // Shuffle the characters in the password
        Random random = new Random();
        password = password.OrderBy(c => random.Next()).ToArray();

        return new string(password);
    }
}