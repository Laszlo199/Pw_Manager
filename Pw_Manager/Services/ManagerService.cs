using Pw_Manager.IRepository;
using Pw_Manager.IServices;
using Pw_Manager.Models;
using Pw_Security.IRepository;

namespace Pw_Manager.Services;

public class ManagerService: IManagerService
{
    private readonly IManagerRepository _repo;
    private readonly IUserRepository _userRepository;

    public ManagerService(IManagerRepository repo, IUserRepository userRepository)
    {
        _repo = repo;
        _userRepository = userRepository;
    }
    public List<PasswordModel> GetAllPasswordsByUserId(int userId)
    {
        if (userId < 0) throw new InvalidDataException("userId cannot be 0");
        return _repo.GetAllPasswordsByUserId(userId);
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