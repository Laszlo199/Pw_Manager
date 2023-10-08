using Pw_Manager.Db.Entity;
using Pw_Manager.Models;

namespace Pw_Manager.Db;

public class ManagerSeeder
{
    private readonly ManagerContext _context;

    public ManagerSeeder(ManagerContext context)
    {
        _context = context;
    }
    public void SeedDevelopment()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        _context.SaveChanges();
        
        _context.Passwords.Add(new PasswordModel()
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