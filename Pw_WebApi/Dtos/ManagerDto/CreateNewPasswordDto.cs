namespace Pw_WebApi.Dtos.ManagerDto;

public class CreateNewPasswordDto
{
    public string WebsiteName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int UserId { get; set; }
}