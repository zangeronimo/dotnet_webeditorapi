using Culinary.LegacyImporter.Legacy.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Nexora.Domain.ValueObjects;

namespace Culinary.LegacyImporter.Legacy.mapping;

public class LegacyRecipeMapping : IEntityTypeConfiguration<LegacyRecipe>
{
    public void Configure(EntityTypeBuilder<LegacyRecipe> builder)
    {
        builder.ToTable("recipes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(c => c.Slug)
            .HasConversion(v => v.Value, v => Slug.Create(v))
            .HasColumnName("slug")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name");

        builder.Property(x => x.Ingredients)
            .HasColumnName("ingredients");

        builder.Property(x => x.Preparation)
            .HasColumnName("preparation");

        builder.Property(c => c.Active).HasColumnName("active").HasConversion<int>().IsRequired();

        builder.Property(c => c.CategoryId).HasColumnName("recipe_categories_id");
        builder.Property(c => c.CompanyId).HasColumnName("webeditor_companies_id");

        builder.HasMany(x => x.Images).WithOne(x => x.Recipe).HasForeignKey(x => x.RecipeId);
    }
}

