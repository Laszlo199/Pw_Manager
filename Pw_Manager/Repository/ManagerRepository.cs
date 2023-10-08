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
        return _context.Passwords.Where(p => p.user.Id == userId).ToList();
    }

    public PasswordsModel Create(PasswordsModel newPassword)
    {
        throw new NotImplementedException();
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