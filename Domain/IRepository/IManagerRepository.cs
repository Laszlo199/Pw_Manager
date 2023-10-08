using Core.Models;

namespace Domain.IRepository;

public interface IManagerRepository
{
    List<Passwords> GetAllPasswordsByUserId(int id);
    Passwords Create(Passwords newPassword);
    Passwords Delete(int passwordId);
    Passwords Update(Passwords password);
    string RandomPasswordGenerator(int length);
}