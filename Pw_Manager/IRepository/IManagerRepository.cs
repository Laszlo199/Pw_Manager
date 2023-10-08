using Pw_Manager.Models;

namespace Pw_Manager.IRepository;

public interface IManagerRepository
{
    List<PasswordModel> GetAllPasswordsByUserId(int id);
    PasswordModel Create(PasswordModel newPassword);
    PasswordModel Delete(int passwordId);
    PasswordModel Update(PasswordModel password);
    string RandomPasswordGenerator(int length);
}