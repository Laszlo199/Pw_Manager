using DataAcces.Entity;

namespace DataAcces;

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

        var user1 = new UserEntity()
        {
            Email = "user1",
            PasswordHash = new byte[64],
            PasswordSalt = new byte[64]
        };

        _context.Add(user1);
        
        _context.Passwords.Add(new PasswordEntity()
        {
            UserId = 1,
            WebsiteName = "Gmail",
            Email = "user1",
            Password = "asd123",
            DateCreated = DateTime.Today,
            UserEntity = user1,
        });
        
        _context.Passwords.Add(new PasswordEntity()
        {
            UserId = 1,
            WebsiteName = "Facebook",
            Email = "user1",
            Password = "Facebook",
            DateCreated = DateTime.Today,
            UserEntity = user1,
        });
        _context.SaveChanges();
    }
}