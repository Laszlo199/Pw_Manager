using Pw_Manager.Db;
using Pw_Manager.IRepository;
using Pw_Manager.IServices;
using Pw_Manager.Model;

namespace Pw_Manager.Services;

public class ManagerService: IManagerService
{
    public List<PasswordsModel> GetAllPasswordsByUserId(int id)
    {
        throw new NotImplementedException();
    }

    public PasswordsModel Create(PasswordsModel newPassword)
    {
        throw new NotImplementedException();
    }

    public PasswordsModel Delete(int passwordId)
    {
        throw new NotImplementedException();
    }

    public PasswordsModel Update(PasswordsModel password)
    {
        throw new NotImplementedException();
    }

    public string RandomPasswordGenerator(int length)
    {
        throw new NotImplementedException();
    }
}