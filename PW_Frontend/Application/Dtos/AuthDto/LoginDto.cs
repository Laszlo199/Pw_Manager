using System.ComponentModel.DataAnnotations;
namespace PW_Frontend.Application.Dtos.AuthDto;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }
    public string Password { get; set; }
}