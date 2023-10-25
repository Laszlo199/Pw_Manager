namespace Core.Models;

public class PasswordModel {
    public int Id { get; set; }
    public string WebsiteName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateCreated { get; set; }
    public User User { get; set; }
}