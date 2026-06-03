using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.ValueObjects;

namespace Nexora.Infrastructure.Persistence.Mappings.Culinary;


public class TagMapping : EntityMapping<Tag>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);
        builder.ToTable("culinary_tags");
        builder.Property(c => c.Slug)
            .HasConversion(v => v.Value, v => Slug.Create(v))
            .HasColumnName("slug")
            .HasMaxLength(150)
            .IsRequired();
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(300);
        builder.Property(c => c.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        builder.Property(c => c.CompanyId).HasColumnName("core_companies_id").IsRequired();

        builder.HasIndex(x => new { x.CompanyId, x.Slug }).IsUnique().HasFilter("\"deleted_at\" IS NULL");
        builder.HasIndex(x => new { x.CompanyId, x.Name }).IsUnique().HasFilter("\"deleted_at\" IS NULL");
        builder.HasIndex(x => new { x.CompanyId, x.Status });
        builder.HasIndex(x => x.CompanyId);
        builder.HasIndex(x => x.DeletedAt);
    }
}