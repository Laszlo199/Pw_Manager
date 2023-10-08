using Microsoft.EntityFrameworkCore;
using Pw_Manager.Models;

namespace Pw_Manager.Db;

public class ManagerContext: DbContext
{
    public ManagerContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }

    public DbSet<PasswordModel> PasswordModel { get; set; }
}