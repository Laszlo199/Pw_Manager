namespace Pw_WebApi.Dtos.ManagerDto;

public class UpdatePasswordDto
{
    public int Id { get; set; }
    public string WebsiteName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}