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

        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.Property<DateTime?>("deleted_at");

        builder.HasOne<Company>().WithMany().HasForeignKey(u => u.CompanyId).HasConstraintName("WebeditorCompanies");

        // Global filter: get only registers not deleted
        builder.HasQueryFilter(c => EF.Property<DateTime?>(c, "deleted_at") == null);
    }
}
