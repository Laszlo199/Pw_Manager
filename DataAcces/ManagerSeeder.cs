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
        
        var password = "password123";
        var password2 = "asd123";
        var email = "user1@gmail.com";
        var websiteName = "Gmail";
        var websiteName2 = "Facebook";
        var transformer = new Transformer.Transformer();
        var user1 = new UserEntity()
        {
            Email = transformer.EncryptPassword(email),
            PasswordHash = new byte[64],
            PasswordSalt = new byte[64]
        };

        _context.Add(user1);
        _context.Passwords.Add(new PasswordEntity()
        {
            WebsiteName = transformer.EncryptPassword(websiteName),
            Email = transformer.EncryptPassword(email),
            Password = transformer.EncryptPassword(password),
            DateCreated = DateTime.Today,
            UserEntity = user1,
        });
        
        _context.Passwords.Add(new PasswordEntity()
        {
            WebsiteName = transformer.EncryptPassword(websiteName2),
            Email = transformer.EncryptPassword(email),
            Password = transformer.EncryptPassword(password2),
            DateCreated = DateTime.Today,
            UserEntity = user1,
        });
        _context.SaveChanges();
    }
}