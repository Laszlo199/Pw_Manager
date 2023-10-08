using Pw_Manager.IServices;
using Pw_Manager.Models;

namespace Pw_Manager.Services;

public class ManagerService: IManagerService
{
    public List<PasswordModel> GetAllPasswords()
    {
        throw new NotImplementedException();
    }

    public PasswordModel Create(PasswordModel newPassword)
    {
        throw new NotImplementedException();
    }

    public PasswordModel Delete(int passwordId)
    {
        throw new NotImplementedException();
    }

    public PasswordModel Update(PasswordModel password)
    {
        throw new NotImplementedException();
    }

    public string RandomPasswordGenerator()
    {
        throw new NotImplementedException();
    }
}