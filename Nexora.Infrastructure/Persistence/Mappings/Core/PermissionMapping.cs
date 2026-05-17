using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Domain.Entities.Core;

namespace Nexora.Infrastructure.Persistence.Mappings.Core;

public class PermissionMapping : EntityMapping<Permission>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        base.Configure(builder);
        builder.ToTable("core_permissions");

        builder.Property(c => c.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        builder.Property(c => c.Label).HasColumnName("label").HasMaxLength(100).IsRequired();
        builder.Property(c => c.Status).HasColumnName("status").HasConversion<byte>().HasColumnType("smallint").IsRequired();
        builder.Property(r => r.ModuleId).HasColumnName("module_id").IsRequired();

        builder.HasOne(p => p.Module)
            .WithMany(m => m.Permissions)
            .HasForeignKey(p => p.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.RolePermissions)
            .WithOne(cm => cm.Permission)
            .HasForeignKey(cm => cm.PermissionId);
    }
}
