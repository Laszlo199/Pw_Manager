using DataAcces.Entity;
using DataAcces.Transformer;

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
        
        const string websiteName = "VeryLegitWebsite.com";
        const string email = "3rJKnD1u9vjXYZ64/nJNFOxnaCUHKJp/6Ix6A11yQlA=";
        const string password = "byIl5GvwHpkIYxXejybIOT9KlLlnlZCqLGSUQdxwQ5Y=";
        
        const string websiteName2 = "Facebook";
        const string email2 = "tD19HGRtoA6Co4pHW+d2xD9KlLlnlZCqLGSUQdxwQ5Y=";
        const string password2 = "NCcm1qx0QIHNxGHgdpzkVz9KlLlnlZCqLGSUQdxwQ5Y=";
        
        const string userEmail = "user@gmail.com";
        const string userPassHash = "Si50ZDKhK6bqi83VLA15+Fg34SiPsCsW5raC1cHxnTr4OAxolduahOEURX+ohXiUGE7VswmLWRgYcjYfdhUbGA==";
        const string userSaltHash = "Y7XVEOVXI1ANxdjzacrv6Q==";
        
        UserEntity user1 = new() {
            Email = userEmail,
            PasswordHash = Convert.FromBase64String(userPassHash),
            PasswordSalt = Convert.FromBase64String(userSaltHash)
        };

        _context.Add(user1);
        
        _context.Passwords.Add(new PasswordEntity
        {
            WebsiteName = websiteName,
            Email = email,
            Password = password,
            DateCreated = DateTime.Today,
            UserEntity = user1,
        });
        
        _context.Passwords.Add(new PasswordEntity
        {
            WebsiteName = websiteName2,
            Email = email2,
            Password = password2,
            DateCreated = DateTime.Today,
            UserEntity = user1,
        });
       
        
        _context.SaveChanges();
    }
}