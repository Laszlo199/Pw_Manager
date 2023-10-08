using Pw_Manager.Model;

namespace Pw_Manager.Db;

public class ManagerSeeder
{
    private readonly PwManagerContext _context;


    public ManagerSeeder(PwManagerContext context)
    {
        _context = context;
    }
    public void SeedDevelopment()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        _context.SaveChanges();
        
        _context.Passwords.Add(new PasswordsModel()
        {
            UserId = 1,
            WebsiteName = "Gmail",
            Email = "user1",
            Password = "asd123",
            DateCreated = DateTime.Today,
        });
        _context.SaveChanges();
    }
}