using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

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

        builder.HasOne<Module>()
            .WithMany(m => m.Permissions)
            .HasForeignKey(p => p.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
