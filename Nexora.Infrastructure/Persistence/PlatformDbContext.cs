using Microsoft.EntityFrameworkCore;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Entities.System;
using Nexora.Infrastructure.Persistence.Mappings.Core;
using Nexora.Infrastructure.Persistence.Mappings.System;

namespace Nexora.Infrastructure.Persistence;

public class PlatformDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<UserCompany> UserCompanies { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserCompanyModuleRole> UserCompanyModuleRoles { get; set; }

    public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new CompanyMapping());
        modelBuilder.ApplyConfiguration(new CompanyModuleMapping());
        modelBuilder.ApplyConfiguration(new UserCompanyMapping());
        modelBuilder.ApplyConfiguration(new ModuleMapping());
        modelBuilder.ApplyConfiguration(new PermissionMapping());
        modelBuilder.ApplyConfiguration(new RoleMapping());
        modelBuilder.ApplyConfiguration(new RolePermissionMapping());
        modelBuilder.ApplyConfiguration(new UserCompanyModuleRoleMapping());
    }
}