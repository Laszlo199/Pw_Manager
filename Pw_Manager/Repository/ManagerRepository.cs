using Pw_Manager.Db;
using Pw_Manager.Db.Entity;
using Pw_Manager.IRepository;
using Pw_Manager.Models;

namespace Pw_Manager.Repository;

public class ManagerRepository: IManagerRepository
{
    private readonly ManagerContext _context;

    public ManagerRepository(ManagerContext context)
    {
        _context = context;
    }
    
    public List<PasswordModel> GetAllPasswordsByUserId(int userId)
    {
        return _context.Passwords.Where(p => p.UserId == userId).ToList();
    }

    public PasswordModel Create(PasswordModel newPassword)
    {
        throw new NotImplementedException();
    }

    public PasswordModel Delete(int passwordId)
    {
        throw new NotImplementedException();
    }

    public PasswordModel Update(PasswordModel password)
    {
        throw new NotImplementedException();
    }

    public string RandomPasswordGenerator(int length)
    {
        throw new NotImplementedException();
    }
}