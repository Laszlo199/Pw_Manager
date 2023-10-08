using Pw_Manager.Models;

namespace Pw_Manager.IServices;

public interface IManagerService
{
    List<PasswordModel> GetAllPasswords();
    PasswordModel Create(PasswordModel newPassword);
    PasswordModel Delete(int passwordId);
    PasswordModel Update(PasswordModel password);
    string RandomPasswordGenerator();
}