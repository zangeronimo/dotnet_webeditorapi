using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.System;


public class ModuleMapping : EntityMapping<Module>
{
    public override void Configure(EntityTypeBuilder<Module> builder)
    {
        base.Configure(builder);
        builder.ToTable("webeditor_modules");

        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(30).IsRequired();

        // 1:N with Roles relationship
        builder.HasMany(c => c.Roles)
               .WithOne()
               .HasForeignKey(r => r.ModuleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
