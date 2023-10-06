using Pw_Security.Helper;
using Pw_Security.Models;

namespace Pw_Security.Db;

public class SecuritySeeder
{
    public void SeedDevelopment(SecurityContext context)
    {
        context.Database.EnsureDeleted();
        
        context.Database.EnsureCreated();

        context.SaveChanges();

       
        var password = "password123"; 
        var authenticationHelper = new AuthHelper(); 
        //Since I'm seeding data I create a password hash + salt manually:
        authenticationHelper.CreatePasswordHash(password, out var pass, out var salt);
        context.LoginUsers.Add(new LoginUser()
        {
            Email = "user1",
            PasswordHash = pass,
            PasswordSalt = salt,
        });
        context.SaveChanges();
    }
}