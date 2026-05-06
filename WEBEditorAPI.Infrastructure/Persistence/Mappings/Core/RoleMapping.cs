using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

public class RoleMapping : EntityMapping<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);
        builder.ToTable("core_roles");

        builder.Property(r => r.Name).HasColumnName("name").HasMaxLength(20).IsRequired();
        builder.Property(r => r.Status).HasColumnName("status").HasConversion<byte>().HasColumnType("smallint").IsRequired();
        builder.Property(r => r.CompanyId).HasColumnName("company_id").IsRequired();

        builder.HasOne<Company>().WithMany().HasForeignKey(r => r.CompanyId).HasConstraintName("FK_core_roles_company_id");

        builder.Metadata
            .FindNavigation(nameof(Role.RolePermissions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
