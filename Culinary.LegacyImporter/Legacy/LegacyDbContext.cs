using Culinary.LegacyImporter.Legacy.Entities;
using Culinary.LegacyImporter.Legacy.mapping;

using Microsoft.EntityFrameworkCore;

namespace Culinary.LegacyImporter;

public sealed class LegacyDbContext : DbContext
{
    public DbSet<LegacyLevel> Levels => Set<LegacyLevel>();
    public DbSet<LegacyCategory> Categories => Set<LegacyCategory>();
    public DbSet<LegacyRecipe> Recipes => Set<LegacyRecipe>();
    public DbSet<LegacyRecipeRating> RecipeRatings => Set<LegacyRecipeRating>();

    public LegacyDbContext(DbContextOptions<LegacyDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LegacyLevelMapping());
        modelBuilder.ApplyConfiguration(new LegacyCategoryMapping());
        modelBuilder.ApplyConfiguration(new LegacyRecipeMapping());
        modelBuilder.ApplyConfiguration(new LegacyRecipeImageMapping());
        modelBuilder.ApplyConfiguration(new LegacyRecipeRatingMapping());
    }
}