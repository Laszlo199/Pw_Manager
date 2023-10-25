using System.Net;
using System.Security.Cryptography;
using Core.Models;
using DataAcces.Entity;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Pw_WebApi.Exceptions;

namespace DataAcces.Repo;

public class ManagerRepository: IManagerRepository
{
    private readonly PwManagerContext _ctx;
    
    public ManagerRepository(PwManagerContext context)
    {
        _ctx = context;
       
    }
    public List<PasswordModel> GetAllPasswordsByUserId(int userId)
    {
        var passwords = _ctx.Passwords
            .Where(c => c.UserEntity.Id == userId)
            .Select(ca => new PasswordModel
            {
                Id = ca.Id,
                Email = ca.Email,
                WebsiteName = ca.WebsiteName,
                Password = ca.Password,
            }).ToList();
        return passwords;
    }

    public PasswordModel Create(PasswordModel newPassword) {
        var userEntity = _ctx.Users.FirstOrDefault(user => user.Id == newPassword.User.Id);
        if (userEntity is null) {
            throw new RestException(HttpStatusCode.NotFound, "User Not found");
        }
        var model = new PasswordEntity {
            Id = newPassword.Id,
            Email = newPassword.Email,
            WebsiteName = newPassword.WebsiteName,
            Password = newPassword.Password,
            DateCreated = DateTime.Today,
            UserEntity = userEntity
        };
        _ctx.Attach(model).State = EntityState.Added;
        _ctx.SaveChanges();
        return newPassword;
    }

    public void Delete(int passwordId) {
        var passwordToDelete = _ctx.Passwords
            .Include(de=>de.UserEntity)
            .FirstOrDefault(d => d.Id == passwordId);
        if (passwordToDelete is null) {
            throw new RestException(HttpStatusCode.NotFound, "Password Not found");
        }
        _ctx.Passwords.Remove(passwordToDelete);
        _ctx.SaveChanges();
    }

    public PasswordModel Update(PasswordModel password)
    {
        _ctx.Attach(new PasswordEntity
        {
            Id = password.Id,
            WebsiteName = password.WebsiteName,
            Email = password.Email,
            Password = password.Password
        }).State = EntityState.Modified;
        _ctx.SaveChanges();

        return password;
    }
    
    public GeneratedPasswordModel RandomPasswordGenerator(int length)
    {
     const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
     const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
     const string numericChars = "0123456789";
     const string specialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?";
     
     const string allChars = lowercaseChars + uppercaseChars + numericChars + specialChars;
        
        string charSet = allChars;

        // Generate a random password with RNGCryptoServiceProvider
        char[] password = new char[length];
        using (RNGCryptoServiceProvider rng = new())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);

            for (int i = 0; i < length; i++)
            {
                password[i] = charSet[randomBytes[i] % charSet.Length];
            }
        }

        // Shuffle the characters in the password
        Random random = new();
        password = password.OrderBy(c => random.Next()).ToArray();

        return new GeneratedPasswordModel {
            Password = new string(password),
            Length = length
        };
    }

    public PasswordModel GetPasswordById( int id) {
        var entity = _ctx.Passwords.FirstOrDefault(pass => pass.Id == id);
        if (entity is null) {
            throw new RestException(HttpStatusCode.NotFound, "Password Not found");
        }
        return new PasswordModel {
            Id = entity.Id,
            Email = entity.Email,
            WebsiteName = entity.WebsiteName,
            Password = entity.Password
        };
    }
}