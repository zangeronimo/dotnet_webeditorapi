using Microsoft.EntityFrameworkCore;
using Nexora.Domain.Entities.Culinary;
using Nexora.Infrastructure.Persistence.Mappings.Culinary;

namespace Nexora.Infrastructure.Persistence;


public class CulinaryDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeRating> RecipeRatings { get; set; }

    public CulinaryDbContext(DbContextOptions<CulinaryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryMapping());
        modelBuilder.ApplyConfiguration(new TagMapping());
        modelBuilder.ApplyConfiguration(new RecipeMapping());
        modelBuilder.ApplyConfiguration(new RecipeRatingMapping());
    }
}
