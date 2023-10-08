using Microsoft.EntityFrameworkCore;
using Pw_Manager.Model;

namespace Pw_Manager.Db;

public class PwManagerContext: DbContext
{
    public PwManagerContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }

    public DbSet<PasswordsModel> Passwords { get; set; }
    public DbSet<User> users { get; set; }
}