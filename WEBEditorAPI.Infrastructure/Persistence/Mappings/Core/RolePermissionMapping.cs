using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

public class RolePermissionMapping : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("core_role_permissions");

        builder.HasKey(cm => new { cm.RoleId, cm.PermissionId });

        builder.Property(cm => cm.RoleId).HasColumnName("role_id");
        builder.Property(cm => cm.PermissionId).HasColumnName("permission_id");

        builder.HasOne(cm => cm.Role)
            .WithMany(c => c.RolePermissions)
            .HasForeignKey(cm => cm.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cm => cm.Permission)
            .WithMany(m => m.RolePermissions)
            .HasForeignKey(cm => cm.PermissionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
