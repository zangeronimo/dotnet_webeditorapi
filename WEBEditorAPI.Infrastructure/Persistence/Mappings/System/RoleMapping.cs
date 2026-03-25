using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.System;


public class RoleMapping : EntityMapping<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);
        builder.ToTable("webeditor_roles");

        builder.Property(u => u.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
        builder.Property(u => u.Label).HasColumnName("label").HasMaxLength(150).IsRequired();
        builder.Property(c => c.ModuleId).HasColumnName("webeditor_modules_id").IsRequired();
    }
}
