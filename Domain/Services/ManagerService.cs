using System.Net;
using Core.IServices;
using Core.Models;
using Domain.IRepository;
using Pw_WebApi.Exceptions;

namespace Domain.Services;

public class ManagerService: IManagerService {
    private readonly IManagerRepository _repo;
    private readonly IUserRepository _userRepository;

    public ManagerService(IManagerRepository repo, IUserRepository userRepository) {
        _repo = repo;
        _userRepository = userRepository;
    }
    
    public List<PasswordModel> GetAllPasswordsByUserId(int userId) {
        return _repo.GetAllPasswordsByUserId(userId);
    }

    public PasswordModel Create(PasswordModel newPassword) {
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == newPassword.User.Id);
        if (user is null)
            throw new RestException(HttpStatusCode.NotFound, "User not found");
        newPassword.User = user;
        return _repo.Create(newPassword);
    }

    public void Delete(int passwordId){
        _repo.Delete(passwordId);
    }

    public PasswordModel Update(PasswordModel password) {
        return _repo.Update(password);
    }

    public GeneratedPasswordModel RandomPasswordGenerator(int length) {
        return _repo.RandomPasswordGenerator(length);
    }

    public PasswordModel GetPasswordById(int passwordId) {
        return _repo.GetPasswordById(passwordId);
    }
}