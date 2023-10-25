using Core.Models;

namespace Core.IServices;

public interface IManagerService {
    List<PasswordModel> GetAllPasswordsByUserId(int id);
    PasswordModel Create(PasswordModel newPassword);
    void Delete(int passwordId);
    PasswordModel Update(PasswordModel password);
    GeneratedPasswordModel RandomPasswordGenerator(int length);
    PasswordModel GetPasswordById(int id);
    

}