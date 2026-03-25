using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.System;


public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("webeditor_users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("id").IsRequired();
        builder.Property(u => u.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value).HasColumnName("email").HasMaxLength(150).IsRequired();
        });

        builder.OwnsOne(u => u.Password, password =>
        {
            password.Property(p => p.Hash).HasColumnName("password").HasMaxLength(200).IsRequired();
            password.Property(p => p.Salt).HasColumnName("salt").HasMaxLength(50).IsRequired();
        });
        builder.Property(c => c.CompanyId).HasColumnName("webeditor_companies_id").IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at").IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(c => c.UpdatedAt)
            .HasColumnName("updated_at").IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property<DateTime?>("deleted_at");

        builder.HasOne<Company>().WithMany().HasForeignKey(u => u.CompanyId).HasConstraintName("WebeditorCompanies");

        // Global filter: get only registers not deleted
        builder.HasQueryFilter(c => EF.Property<DateTime?>(c, "deleted_at") == null);

        // Many-to-Many unidirectional relationship
        builder.HasMany(c => c.Roles)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "webeditor_users_has_webeditor_roles",
                j => j.HasOne<Role>()
                      .WithMany()
                      .HasForeignKey("webeditor_roles_id")
                      .HasConstraintName("FK_6861fc587e05e663cba628d4156"),
                j => j.HasOne<User>()
                      .WithMany()
                      .HasForeignKey("webeditor_users_id")
                      .HasConstraintName("FK_a34bc58611284ad084a88dec663"),
                j =>
                {
                    j.HasKey("webeditor_users_id", "webeditor_roles_id");
                });
    }
}
