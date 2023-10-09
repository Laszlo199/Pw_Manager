using Core.IServices;
using Core.Models;
using Domain.IRepository;

namespace Domain.Services;

public class ManagerService: IManagerService
{
    private readonly IManagerRepository _repo;
    //private readonly IUserRepository _userRepository;

    public ManagerService(IManagerRepository repo)
    {
        _repo = repo;
    }
    public List<Passwords> GetAllPasswordsByUserId(int userId)
    {
        if (userId < 0) throw new InvalidDataException("userId cannot be less than 0");
        return _repo.GetAllPasswordsByUserId(userId);
    }

    public Passwords Create(Passwords newPassword)
    {
        throw new NotImplementedException();
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
        return _repo.RandomPasswordGenerator(length);
    }
}