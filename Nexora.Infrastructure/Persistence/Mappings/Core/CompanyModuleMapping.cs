using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities.Core;

namespace Nexora.Infrastructure.Persistence.Mappings.Core;

public class CompanyModuleMapping : IEntityTypeConfiguration<CompanyModule>
{
    public void Configure(EntityTypeBuilder<CompanyModule> builder)
    {
        builder.ToTable("core_company_modules");

        builder.HasKey(cm => new { cm.CompanyId, cm.ModuleId });

        builder.Property(cm => cm.CompanyId).HasColumnName("company_id");
        builder.Property(cm => cm.ModuleId).HasColumnName("module_id");

        builder.HasOne(cm => cm.Company)
            .WithMany(c => c.CompanyModules)
            .HasForeignKey(cm => cm.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cm => cm.Module)
            .WithMany(m => m.CompanyModules)
            .HasForeignKey(cm => cm.ModuleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
