using Core.Models;

namespace Domain.IRepository;

public interface IManagerRepository
{
    List<PasswordModel> GetAllPasswordsByUserId(int id);
    PasswordModel Create(PasswordModel newPassword);
    void Delete(int passwordId);
    PasswordModel Update(PasswordModel password);
    string RandomPasswordGenerator(int length);
    PasswordModel GetPasswordById(int id);
}