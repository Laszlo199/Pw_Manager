namespace Pw_WebApi.Dtos;

public class GetAllByUserIdDto
{
    public string WebsiteName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateCreated { get; set; }
}