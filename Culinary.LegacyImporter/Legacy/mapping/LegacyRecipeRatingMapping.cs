using Culinary.LegacyImporter.Legacy.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Culinary.LegacyImporter.Legacy.mapping;

public class LegacyRecipeRatingMapping : IEntityTypeConfiguration<LegacyRecipeRating>
{
    public void Configure(EntityTypeBuilder<LegacyRecipeRating> builder)
    {
        builder.ToTable("recipe_ratings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Rate)
            .HasColumnName("rate");

        builder.Property(x => x.Comment)
            .HasColumnName("comment");

        builder.Property(x => x.Name)
            .HasColumnName("name");

        builder.Property(c => c.Active).HasColumnName("active").HasConversion<int>().IsRequired();

        builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");

        builder.Property(c => c.RecipeId).HasColumnName("recipes_id");
        builder.Property(c => c.CompanyId).HasColumnName("webeditor_companies_id");
    }
}
