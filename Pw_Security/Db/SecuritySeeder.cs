using Pw_Security.Helper;
using Pw_Security.Models;

namespace Pw_Security.Db;

public class SecuritySeeder
{
    private readonly SecurityContext _context;

    public SecuritySeeder(SecurityContext context)
    {
        _context = context;
    }
    public void SeedDevelopment()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        _context.SaveChanges();

       
        var password = "password123"; 
        var authenticationHelper = new AuthHelper(); 
        //Since I'm seeding data I create a password hash + salt manually:
        authenticationHelper.CreatePasswordHash(password, out var pass, out var salt);
        _context.LoginUsers.Add(new LoginUser()
        {
            Email = "user1",
            PasswordHash = pass,
            PasswordSalt = salt,
        });
        _context.SaveChanges();
    }
}