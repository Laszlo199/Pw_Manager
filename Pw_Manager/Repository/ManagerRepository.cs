using Microsoft.EntityFrameworkCore;
using Pw_Manager.Db;
using Pw_Manager.IRepository;
using Pw_Manager.Model;

namespace Pw_Manager.Repository;

public class ManagerRepository: IManagerRepository
{
    private readonly PwManagerContext _context;

    public ManagerRepository(PwManagerContext context)
    {
        _context = context;
    }
    public List<PasswordsModel> GetAllPasswordsByUserId(int userId)
    {
        return _context.Passwords.Where(p => p.UserModel.Id == userId).ToList();
    }

    public PasswordsModel Create(PasswordsModel newPassword)
    {
        var model = new PasswordsModel()
        {
            WebsiteName = newPassword.WebsiteName,
            Email = newPassword.Email,
            Password = newPassword.Password,
            UserId = newPassword.UserModel.Id,
            UserModel = new User()
            {
                Id = 
            }
            
        };
        _ctx.Cards.Attach(newEntity).State = EntityState.Added;
        _ctx.SaveChanges();
        return newCard;
    }

    public PasswordsModel Delete(int passwordId)
    {
        throw new NotImplementedException();
    }

    public PasswordsModel Update(PasswordsModel password)
    {
        throw new NotImplementedException();
    }

    public string RandomPasswordGenerator(int length)
    {
        throw new NotImplementedException();
    }
}