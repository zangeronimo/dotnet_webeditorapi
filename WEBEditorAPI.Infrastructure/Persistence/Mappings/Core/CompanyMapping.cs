using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

public class CompanyMapping : EntityMapping<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);
        builder.ToTable("core_companies");

        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        builder.Property(c => c.Status).HasColumnName("status").HasConversion<byte>().HasColumnType("smallint").IsRequired();
    }
}