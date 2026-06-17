using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Nexora.Domain.Entities.Core;

namespace Nexora.Infrastructure.Persistence.Mappings.Core;

public class ApiClientMapping : EntityMapping<ApiClient>
{
    public override void Configure(EntityTypeBuilder<ApiClient> builder)
    {
        base.Configure(builder);
        builder.ToTable("core_api_clients");

        builder.Property(u => u.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        builder.Property(u => u.ClientId).HasColumnName("client_id").HasMaxLength(100);
        builder.Property(u => u.EncryptedSecret).HasColumnName("encrypted_secret").HasMaxLength(500);
        builder.Property(u => u.CompanyId).HasColumnName("company_id").IsRequired();
        builder.Property(u => u.Status).HasColumnName("status").HasConversion<byte>().HasColumnType("smallint").IsRequired();

        builder.HasIndex(x => x.ClientId).IsUnique();
        builder.HasIndex(x => x.CompanyId);
    }
}