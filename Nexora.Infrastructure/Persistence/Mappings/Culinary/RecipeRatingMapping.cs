using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities.Culinary;

namespace Nexora.Infrastructure.Persistence.Mappings.Culinary;


public class RecipeRatingMapping : EntityMapping<RecipeRating>
{
    public override void Configure(EntityTypeBuilder<RecipeRating> builder)
    {
        base.Configure(builder);
        builder.ToTable("culinary_recipe_ratings");
        builder.OwnsOne(r => r.Score, score =>
        {
            score.Property(m => m.Value).HasColumnName("score").HasColumnType("integer");
        });
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(150);
        builder.Property(x => x.Comment)
            .HasColumnName("comment")
            .HasMaxLength(500);
        builder.Property(c => c.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        builder.Property(r => r.PublishedAt).HasColumnName("published_at");
        builder.Property(c => c.RecipeId).HasColumnName("culinary_recipes_id").IsRequired();
        builder.Property(c => c.CompanyId).HasColumnName("core_companies_id").IsRequired();

        builder.HasIndex(x => new { x.CompanyId, x.Status });
        builder.HasIndex(x => new { x.CompanyId, x.RecipeId, x.Status });
        builder.HasIndex(x => x.PublishedAt);
        builder.HasIndex(x => x.RecipeId);
        builder.HasIndex(x => x.CompanyId);
        builder.HasIndex(x => x.DeletedAt);

        builder.HasOne<Recipe>()
            .WithMany()
            .HasForeignKey(r => r.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}