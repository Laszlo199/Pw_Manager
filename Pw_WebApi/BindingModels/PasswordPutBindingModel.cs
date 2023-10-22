using System.ComponentModel.DataAnnotations;
namespace Pw_WebApi.BindingModels;

public class PasswordPutBindingModel {
    [Required]
    public int Id { get; set; }
    [Required]
    public string WebsiteName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}