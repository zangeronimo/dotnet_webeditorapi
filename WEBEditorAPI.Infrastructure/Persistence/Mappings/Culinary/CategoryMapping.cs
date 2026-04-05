using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Culinary;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Culinary;


public class CategoryMapping : EntityMapping<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);
        builder.ToTable("recipe_categories");

        builder.OwnsOne(c => c.Slug, slug =>
        {
            slug.Property(s => s.Value).HasColumnName("slug").HasMaxLength(45).IsRequired();
        });
        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(45).IsRequired();
        builder.Property(c => c.Active).HasColumnName("active").HasConversion<int>().IsRequired();
        builder.Property(c => c.LevelId).HasColumnName("recipe_levels_id").IsRequired();
        builder.Property(c => c.CompanyId).HasColumnName("webeditor_companies_id").IsRequired();
    }
}