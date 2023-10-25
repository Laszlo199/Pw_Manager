using System.ComponentModel.DataAnnotations;
namespace Pw_WebApi.BindingModels;

public class PasswordPostBindingModel {
    [Required]
    public string WebsiteName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public int UserId { get; set; }
}