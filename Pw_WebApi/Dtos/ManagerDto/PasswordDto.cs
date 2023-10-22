using Core.Models;
namespace Pw_WebApi.Dtos.ManagerDto;

public class PasswordDto {
    public string WebsiteName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public PasswordDto(PasswordModel model) {
        WebsiteName = model.WebsiteName;
        Email = model.Email;
        Password = model.Password;
    }
}