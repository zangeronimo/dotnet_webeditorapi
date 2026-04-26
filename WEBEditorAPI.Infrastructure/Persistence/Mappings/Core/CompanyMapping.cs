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

        builder.HasMany<Module>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "core_company_modules",
                r => r.HasOne<Module>()
                      .WithMany()
                      .HasForeignKey("module_id")
                      .OnDelete(DeleteBehavior.Restrict),

                l => l.HasOne<Company>()
                      .WithMany()
                      .HasForeignKey("company_id")
                      .OnDelete(DeleteBehavior.Cascade),

                j =>
                {
                    j.HasKey("company_id", "module_id");
                    j.ToTable("core_company_modules");
                }
            );
    }
}