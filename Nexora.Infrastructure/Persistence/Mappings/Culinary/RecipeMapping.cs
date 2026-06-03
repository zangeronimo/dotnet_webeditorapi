using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.ValueObjects;
using Nexora.Infrastructure.Persistence.Converters.Culinary;

namespace Nexora.Infrastructure.Persistence.Mappings.Culinary;

public class RecipeMapping : EntityMapping<Recipe>
{
    public override void Configure(EntityTypeBuilder<Recipe> builder)
    {
        base.Configure(builder);
        builder.ToTable("culinary_recipes");

        builder.Property(c => c.Slug)
            .HasConversion(v => v.Value, v => Slug.Create(v))
            .HasColumnName("slug")
            .HasMaxLength(150)
            .IsRequired();
        builder.Property(r => r.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
        builder.OwnsOne(r => r.Content, content =>
        {
            content.Property(p => p.ShortDescription).HasColumnName("short_description").HasMaxLength(255);
            content.Property(p => p.FullDescription).HasColumnName("full_description");
            content.Property(p => p.Ingredients)
                .HasConversion(RecipeJsonConverters.IngredientsConverter)
                .HasColumnName("ingredients")
                .HasColumnType("jsonb")
                .IsRequired();
            content.Property(p => p.Steps)
                .HasConversion(RecipeJsonConverters.StepsConverter)
                .HasColumnName("steps")
                .HasColumnType("jsonb")
                .IsRequired();
            content.Property(p => p.Notes).HasColumnName("notes");
        });
        builder.OwnsOne(r => r.Attributes, attribute =>
        {
            attribute.Property(p => p.Difficulty).HasColumnName("difficulty").HasConversion<int>();
            attribute.Property(p => p.Cuisine).HasColumnName("cuisine").HasMaxLength(100);
        });
        builder.OwnsOne(r => r.Yield, yield =>
        {
            yield.Property(y => y.YieldTotal).HasColumnName("yield_total").HasMaxLength(50);
        });
        builder.OwnsOne(r => r.Timing, timing =>
        {
            timing.Property(t => t.PrepTime).HasColumnName("prep_time").HasConversion<int>();
            timing.Property(t => t.CookTime).HasColumnName("cook_time").HasConversion<int>();
            timing.Property(t => t.RestTime).HasColumnName("rest_time").HasConversion<int>();
        });
        builder.OwnsOne(r => r.Seo, seo =>
        {
            seo.Property(s => s.MetaTitle).HasColumnName("meta_title").HasMaxLength(70);
            seo.Property(s => s.MetaDescription).HasColumnName("meta_description").HasMaxLength(170);
            seo.Property(s => s.CanonicalUrl).HasColumnName("canonical_url").HasMaxLength(500);
        });
        builder.OwnsOne(r => r.Media, media =>
        {
            media.Property(m => m.ImageUrl).HasColumnName("image_url").HasMaxLength(500);
        });
        builder.Property(r => r.StructuredData).HasColumnName("structured_data").HasColumnType("jsonb");
        builder.Property(r => r.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        builder.Property(r => r.CategoryId).HasColumnName("culinary_category_id").IsRequired();
        builder.Property(r => r.CompanyId).HasColumnName("core_companies_id").IsRequired();
        builder.Property(r => r.PublishedAt).HasColumnName("published_at");
        builder.Property<List<Guid>>("_tagIds").HasColumnName("tag_ids").HasColumnType("uuid[]");

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CompanyId, x.Slug })
            .IsUnique()
            .HasFilter("\"deleted_at\" IS NULL");
        builder.HasIndex(x => new { x.CompanyId, x.Status });
        builder.HasIndex(x => new { x.CompanyId, x.CategoryId });
        builder.HasIndex(x => x.CompanyId);
        builder.HasIndex(x => x.PublishedAt);
        builder.HasIndex(x => x.DeletedAt);
    }
}