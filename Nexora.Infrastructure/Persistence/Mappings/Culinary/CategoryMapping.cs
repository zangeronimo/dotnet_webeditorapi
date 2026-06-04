using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.ValueObjects;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Infrastructure.Persistence.Mappings.Culinary;


public class CategoryMapping : EntityMapping<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);
        builder.ToTable("culinary_categories");
        builder.Property(c => c.Slug)
            .HasConversion(v => v.Value, v => Slug.Create(v))
            .HasColumnName("slug")
            .HasMaxLength(150)
            .IsRequired();
        builder.Property(c => c.Name)
            .HasConversion(v => v.Value, v => new CategoryName(v))
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(500);
        builder.Property(x => x.DisplayOrder)
            .HasColumnName("display_order");
        builder.OwnsOne(r => r.Seo, seo =>
        {
            seo.WithOwner();
            seo.Property(s => s.MetaTitle).HasColumnName("meta_title").HasMaxLength(70);
            seo.Property(s => s.MetaDescription).HasColumnName("meta_description").HasMaxLength(170);
            seo.Property(s => s.CanonicalUrl).HasColumnName("canonical_url").HasMaxLength(500);
        });
        builder.Property(x => x.FeaturedImageUrl).HasColumnName("featured_image_url").HasMaxLength(500);
        builder.Property(c => c.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        builder.Property(c => c.ParentId).HasColumnName("parent_id");
        builder.Property(c => c.CompanyId).HasColumnName("core_companies_id").IsRequired();

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CompanyId, x.Slug }).IsUnique().HasFilter("\"deleted_at\" IS NULL");
        builder.HasIndex(x => new { x.CompanyId, x.ParentId });
        builder.HasIndex(x => new { x.CompanyId, x.Status });
        builder.HasIndex(x => new { x.CompanyId, x.DisplayOrder });
        builder.HasIndex(x => x.CompanyId);
        builder.HasIndex(x => x.DeletedAt);

        builder.Navigation(x => x.Seo).IsRequired();
    }
}