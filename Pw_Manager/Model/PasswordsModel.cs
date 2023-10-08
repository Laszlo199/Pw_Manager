namespace Pw_Manager.Model;

public class PasswordsModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string WebsiteName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } 
    public DateTime DateCreated { get; set; }
    public User User { get; set; }
}