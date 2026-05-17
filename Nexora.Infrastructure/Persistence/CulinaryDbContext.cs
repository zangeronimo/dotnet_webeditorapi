using Microsoft.EntityFrameworkCore;
using Nexora.Domain.Entities.Culinary;
using Nexora.Infrastructure.Persistence.Mappings.Culinary;

namespace Nexora.Infrastructure.Persistence;


public class CulinaryDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public CulinaryDbContext(DbContextOptions<CulinaryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryMapping());
    }
}
