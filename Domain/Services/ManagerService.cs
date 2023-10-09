using Core.IServices;
using Core.Models;
using Domain.IRepository;

namespace Domain.Services;

public class ManagerService: IManagerService
{
    private readonly IManagerRepository _repo;
    private readonly IUserRepository _userRepository;

    public ManagerService(IManagerRepository repo, IUserRepository userRepository)
    {
        _repo = repo;
        _userRepository = userRepository;
    }
    public List<Passwords> GetAllPasswordsByUserId(int userId)
    {
        if (userId < 0) throw new InvalidDataException("userId cannot be less than 0");
        return _repo.GetAllPasswordsByUserId(userId);
    }

    public Passwords Create(Passwords newPassword)
    {
        if (newPassword == null)
            throw new ArgumentNullException();
        if (newPassword.Email is null or "")
            throw new InvalidDataException("Email must be specified");
        if(newPassword.WebsiteName is null or "" )
            throw new InvalidDataException("Name must be specified");
        if(newPassword.Password.Length > 20 && newPassword.Password.Length < 7  )
            throw new InvalidDataException("Password must be bigger 7 character and smaller 20 character");
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == newPassword.User.Id);
        if (user == null)
            throw new InvalidDataException("User doesnt exist");
        newPassword.User = user;
        return _repo.Create(newPassword);
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