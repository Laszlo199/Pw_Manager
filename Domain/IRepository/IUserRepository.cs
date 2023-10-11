using Core.Models;

namespace Domain.IRepository;

public interface IUserRepository
{
    List<User> GetAll();
    bool Create(User user);
}