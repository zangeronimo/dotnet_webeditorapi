using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.System;
using WEBEditorAPI.Infrastructure.Persistence.Mappings.System;

namespace WEBEditorAPI.Infrastructure.Persistence;


public class AppDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyMapping());
    }
}
