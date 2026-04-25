using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace WEBEditorAPI.Infrastructure.Persistence.Mappings;

public abstract class EntityMapping<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedNever();

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at").IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");

        builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");

        // Global filter to soft delete
        builder.HasQueryFilter(e => e.DeletedAt == null);
    }
}