using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Culinary;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Culinary;

public class RatingMapping : EntityMapping<Rating>
{
    public override void Configure(EntityTypeBuilder<Rating> builder)
    {
        base.Configure(builder);
        builder.ToTable("recipe_recipes_ratings");
        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(150);
        builder.Property(c => c.Rate).HasColumnName("rate").HasConversion<int>().IsRequired();
        builder.Property(c => c.Comment).HasColumnName("comment").HasMaxLength(255);
        builder.Property(c => c.Active).HasColumnName("active").HasConversion<int>().IsRequired();
        builder.Property(c => c.RecipeId).HasColumnName("recipe_recipes_id").IsRequired();
        builder.Property(c => c.CompanyId).HasColumnName("webeditor_companies_id").IsRequired();
    }
}
