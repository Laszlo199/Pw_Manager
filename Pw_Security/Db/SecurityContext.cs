using Microsoft.EntityFrameworkCore;
using Pw_Security.Models;

namespace Pw_Security.Db;

public class SecurityContext: DbContext
{
    public SecurityContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }

    public DbSet<LoginUser> LoginUsers { get; set; }
}