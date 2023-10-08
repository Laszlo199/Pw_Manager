using Pw_Manager.Model;

namespace Pw_Manager.IServices;

public interface IManagerService
{
    List<PasswordsModel> GetAllPasswordsByUserId(int id);
    PasswordsModel Create(PasswordsModel newPassword);
    PasswordsModel Delete(int passwordId);
    PasswordsModel Update(PasswordsModel password);
    string RandomPasswordGenerator(int length);
    
}