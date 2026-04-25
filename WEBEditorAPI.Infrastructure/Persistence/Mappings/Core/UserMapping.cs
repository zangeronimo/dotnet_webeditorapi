using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

public class UserMapping : EntityMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.ToTable("core_users");

        builder.Property(u => u.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value).HasColumnName("email").HasMaxLength(200).IsRequired();
            email.HasIndex(e => e.Value).IsUnique().HasDatabaseName("IX_core_users_email");
        });

        builder.OwnsOne(u => u.PasswordHash, password =>
        {
            password.Property(p => p.Hash).HasColumnName("password_hash").HasColumnType("text").IsRequired();
        });
        builder.Property(u => u.Status).HasColumnName("status").HasConversion<byte>().HasColumnType("smallint").IsRequired();
    }
}