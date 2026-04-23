using Microsoft.EntityFrameworkCore;
using WEBEditorAPI.Domain.Entities.Culinary;
using WEBEditorAPI.Infrastructure.Persistence.Mappings.Culinary;

namespace WEBEditorAPI.Infrastructure.Persistence;


public class CulinaryDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    public CulinaryDbContext(DbContextOptions<CulinaryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryMapping());
        modelBuilder.ApplyConfiguration(new LevelMapping());
        modelBuilder.ApplyConfiguration(new RecipeMapping());
        modelBuilder.ApplyConfiguration(new RatingMapping());
    }
}
