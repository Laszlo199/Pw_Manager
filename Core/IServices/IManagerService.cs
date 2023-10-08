using Core.Models;

namespace Core.IServices;

public interface IManagerService
{
    List<Passwords> GetAllPasswordsByUserId(int id);
    Passwords Create(Passwords newPassword);
    Passwords Delete(int passwordId);
    Passwords Update(Passwords password);
    string RandomPasswordGenerator(int length);
    
}