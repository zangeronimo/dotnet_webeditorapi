using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Culinary;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Culinary;

public class LevelMapping : EntityMapping<Level>
{
    public override void Configure(EntityTypeBuilder<Level> builder)
    {
        base.Configure(builder);
        builder.ToTable("recipe_levels");

        builder.OwnsOne(c => c.Slug, slug =>
        {
            slug.Property(s => s.Value).HasColumnName("slug").HasMaxLength(45).IsRequired();
        });
        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(45).IsRequired();
        builder.Property(c => c.Active).HasColumnName("active").HasConversion<int>().IsRequired();
        builder.Property(c => c.CompanyId).HasColumnName("webeditor_companies_id").IsRequired();

        // 1:N with Category relationship
        builder.HasMany(c => c.Categories)
               .WithOne()
               .HasForeignKey(r => r.LevelId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
