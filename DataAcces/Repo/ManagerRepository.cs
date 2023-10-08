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
        return _ctx.Passwords.Where(p => p.UserEntity.Id == userId).ToList();
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
        return model;
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
        throw new NotImplementedException();
    }
}