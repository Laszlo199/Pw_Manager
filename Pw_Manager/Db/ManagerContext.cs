using Microsoft.EntityFrameworkCore;
using Pw_Manager.Models;
using Pw_Security.Db.Entity;

namespace Pw_Manager.Db;

public class ManagerContext: DbContext
{
    public ManagerContext(DbContextOptions<ManagerContext> contextOptions) : base(contextOptions)
    {
    }

    public DbSet<PasswordModel> Passwords { get; set; }
    public DbSet<User> Users { get; set; }
}