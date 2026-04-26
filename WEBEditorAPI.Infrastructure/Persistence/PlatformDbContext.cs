using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

namespace WEBEditorAPI.Infrastructure.Persistence;

public class PlatformDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<UserCompany> UserCompanies { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Role> Roles { get; set; }

    public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new CompanyMapping());
        modelBuilder.ApplyConfiguration(new UserCompanyMapping());
        modelBuilder.ApplyConfiguration(new ModuleMapping());
        modelBuilder.ApplyConfiguration(new PermissionMapping());
        modelBuilder.ApplyConfiguration(new RoleMapping());
    }
}