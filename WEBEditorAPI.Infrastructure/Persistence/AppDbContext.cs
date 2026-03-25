using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Infrastructure.Persistence.Mappings.System;

namespace WEBEditorAPI.Infrastructure.Persistence;


public class AppDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Role> Roles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyMapping());
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new ModuleMapping());
        modelBuilder.ApplyConfiguration(new RoleMapping());
    }
}
