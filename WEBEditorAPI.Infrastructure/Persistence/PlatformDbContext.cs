using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

namespace WEBEditorAPI.Infrastructure.Persistence;

public class PlatformDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMapping());
    }
}