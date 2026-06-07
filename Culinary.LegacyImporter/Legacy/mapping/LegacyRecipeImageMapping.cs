using Culinary.LegacyImporter.Legacy.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Culinary.LegacyImporter.Legacy.mapping;

public class LegacyRecipeImageMapping : IEntityTypeConfiguration<LegacyRecipeImage>
{
    public void Configure(EntityTypeBuilder<LegacyRecipeImage> builder)
    {
        builder.ToTable("recipe_images");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Url)
            .HasColumnName("url");

        builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");

        builder.Property(c => c.RecipeId).HasColumnName("recipes_id");
        builder.Property(c => c.CompanyId).HasColumnName("webeditor_companies_id");

        builder.HasOne(x => x.Recipe).WithMany(x => x.Images).HasForeignKey(x => x.RecipeId);
    }
}
