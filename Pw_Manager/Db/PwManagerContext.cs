using Microsoft.EntityFrameworkCore;
using Pw_Manager.Model;

namespace Pw_Manager.Db;

public class PwManagerContext: DbContext
{
    public PwManagerContext(DbContextOptions<PwManagerContext> contextOptions) : base(contextOptions)
    {
    }

    public DbSet<PasswordsModel> Passwords { get; set; }
    public DbSet<UserModel> users { get; set; }
}