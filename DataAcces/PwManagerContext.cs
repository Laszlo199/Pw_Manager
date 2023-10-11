using DataAcces.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAcces;

public class PwManagerContext: DbContext
{
    public PwManagerContext(DbContextOptions<PwManagerContext> contextOptions) : base(contextOptions)
    {
    }

    public DbSet<PasswordEntity> Passwords { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}