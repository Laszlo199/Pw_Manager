using Pw_Manager.Db;
using Pw_Manager.IRepository;
using Pw_Manager.IServices;
using Pw_Manager.Model;

namespace Pw_Manager.Services;

public class ManagerService: IManagerService
{
    private readonly IManagerRepository _repo;
    //private readonly IUserRepository _userRepository;

    public ManagerService(IManagerRepository repo)
    {
        _repo = repo;
    }
    public List<PasswordsModel> GetAllPasswordsByUserId(int userId)
    {
        if (userId < 0) throw new InvalidDataException("userId cannot be less than 0");
        return _repo.GetAllPasswordsByUserId(userId);
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