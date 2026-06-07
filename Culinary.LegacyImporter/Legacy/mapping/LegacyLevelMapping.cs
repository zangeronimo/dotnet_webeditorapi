using Culinary.LegacyImporter.Legacy.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Nexora.Domain.ValueObjects;

namespace Culinary.LegacyImporter.Legacy.mapping;

public class LegacyLevelMapping : IEntityTypeConfiguration<LegacyLevel>
{
    public void Configure(EntityTypeBuilder<LegacyLevel> builder)
    {
        builder.ToTable("recipe_levels");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(c => c.Slug)
            .HasConversion(v => v.Value, v => Slug.Create(v))
            .HasColumnName("slug")
            .HasMaxLength(45)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name");

        builder.Property(c => c.Active).HasColumnName("active").HasConversion<int>().IsRequired();

        builder.Property(c => c.CompanyId).HasColumnName("webeditor_companies_id");
    }
}