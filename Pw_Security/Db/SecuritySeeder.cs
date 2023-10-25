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

       
        
        const string userEmail = "user@gmail.com";
        //passPASS!20
        const string userPassHash = "Si50ZDKhK6bqi83VLA15+Fg34SiPsCsW5raC1cHxnTr4OAxolduahOEURX+ohXiUGE7VswmLWRgYcjYfdhUbGA==";
        const string userSaltHash = "Y7XVEOVXI1ANxdjzacrv6Q==";
        
        LoginUser user1 = new() {
            Email = userEmail,
            PasswordHash = Convert.FromBase64String(userPassHash),
            PasswordSalt = Convert.FromBase64String(userSaltHash)
        };
        
        _context.LoginUsers.Add(user1);
        _context.SaveChanges();
        
    }
}