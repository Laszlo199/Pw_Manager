using Microsoft.Extensions.Configuration;
using Pw_Security.Helper;
using Pw_Security.Models;

namespace Pw_Security.Db;

public class SecuritySeeder
{
    private readonly SecurityContext _context;
    private readonly IAuthHelper _authHelper;

    public SecuritySeeder(SecurityContext context, IAuthHelper authHelper)
    {
        _context = context;
        _authHelper = authHelper;
    }
   
    public void SeedDevelopment()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        _context.SaveChanges();

       
        var password = "password123";
        var authenticationHelper = _authHelper; 
        //Since I'm seeding data I create a password hash + salt manually:
        authenticationHelper.CreatePasswordHash(password, out var pass, out var salt);
        _context.LoginUsers.Add(new LoginUser()
        {
            Email = "user1@gmail.com",
            PasswordHash = pass,
            PasswordSalt = salt,
        });
        _context.SaveChanges();
        
    }
}