using Pw_Security.Db.Entity;

namespace Pw_Security.IRepository;

public interface IUserRepository
{
    List<User> GetAll();
    bool Create(User user);
}