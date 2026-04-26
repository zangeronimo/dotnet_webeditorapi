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

        builder.OwnsMany(m => m.Permissions, p =>
        {
            p.ToTable("core_permissions");

            p.WithOwner().HasForeignKey("module_id");

            p.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
            p.Property(x => x.Label).HasColumnName("label").HasMaxLength(100).IsRequired();
            p.Property(x => x.Status).HasColumnName("status").HasConversion<byte>().IsRequired();
            p.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            p.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            p.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            p.HasKey(x => x.Id);
            p.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();
        });
    }
}
