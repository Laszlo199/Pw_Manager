using Pw_Manager.Db;
using Pw_Manager.Model;
using Pw_Security.Db;
using Pw_Security.Db.Entity;
using Pw_Security.IRepository;
using Pw_Security.Models;

namespace Pw_Security.Repositories;

public class UserRepository: IUserRepository
{
    private readonly SecurityContext _context;
    private readonly PwManagerContext _pwManagerContext;
    

    public UserRepository(SecurityContext context, PwManagerContext pwManagerContext)
    {
        _context = context;
        _pwManagerContext = pwManagerContext;
    }
    public List<User> GetAll()
    {
        return _context.LoginUsers.Select(u => new User
        {
            Id = u.Id,
            Email = u.Email,
            PasswordHash = u.PasswordHash,
            PasswordSalt = u.PasswordSalt
        }).ToList();
    }

    public bool Create(User user)
    {
        var createdUser = _context.LoginUsers.Add(new LoginUser()
        {
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt
        });
        _context.SaveChanges();

        _pwManagerContext.users.Add(new UserModel()
        {
            Email = user.Email
        });
        _pwManagerContext.SaveChanges();
        return createdUser != null;
    }
}