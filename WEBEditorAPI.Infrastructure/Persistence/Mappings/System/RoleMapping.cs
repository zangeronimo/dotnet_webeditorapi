using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.System;


public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("webeditor_roles");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("id").IsRequired();
        builder.Property(u => u.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
        builder.Property(u => u.Label).HasColumnName("label").HasMaxLength(150).IsRequired();
        builder.Property(c => c.ModuleId).HasColumnName("webeditor_modules_id").IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at").IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(c => c.UpdatedAt)
            .HasColumnName("updated_at").IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property<DateTime?>("deleted_at");

        // Global filter: get only registers not deleted
        builder.HasQueryFilter(c => EF.Property<DateTime?>(c, "deleted_at") == null);
    }
}
