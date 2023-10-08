using Pw_Manager.Models;
using Pw_Security.Db;
using Pw_Security.Db.Entity;
using Pw_Security.Models;

namespace Pw_Manager.Db;

public class ManagerSeeder
{
    private readonly ManagerContext _context;
    private readonly SecurityContext _securityContext;

    public ManagerSeeder(ManagerContext context, SecurityContext securityContext)
    {
        _context = context;
        _securityContext = securityContext;
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