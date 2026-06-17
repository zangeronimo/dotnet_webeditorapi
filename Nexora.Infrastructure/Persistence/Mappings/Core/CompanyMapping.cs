using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Nexora.Domain.Entities.Core;

namespace Nexora.Infrastructure.Persistence.Mappings.Core;

public class CompanyMapping : EntityMapping<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);
        builder.ToTable("core_companies");

        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        builder.Property(c => c.Status).HasColumnName("status").HasConversion<byte>().HasColumnType("smallint").IsRequired();

        builder.Metadata
            .FindNavigation(nameof(Company.CompanyModules))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(c => c.ApiClients)
           .WithOne()
           .HasForeignKey(a => a.CompanyId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Name)
            .IsUnique()
            .HasFilter("\"deleted_at\" IS NULL");
    }
}