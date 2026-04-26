using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

public class ModuleMapping : EntityMapping<Module>
{
    public override void Configure(EntityTypeBuilder<Module> builder)
    {
        base.Configure(builder);
        builder.ToTable("core_modules");

        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(30).IsRequired();
        builder.Property(c => c.Status).HasColumnName("status").HasConversion<byte>().HasColumnType("smallint").IsRequired();

        builder.HasMany(m => m.Permissions)
           .WithOne()
           .HasForeignKey(p => p.ModuleId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
