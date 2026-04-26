using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

public class UserCompanyMapping : EntityMapping<UserCompany>
{
    public override void Configure(EntityTypeBuilder<UserCompany> builder)
    {
        base.Configure(builder);
        builder.ToTable("core_user_companies");

        builder.Property(c => c.NickName).HasColumnName("nickname").HasMaxLength(100);
        builder.Property(c => c.AvatarUrl).HasColumnName("avatar_url");
        builder.Property(e => e.InvitedAt).HasColumnName("invited_at");
        builder.Property(e => e.JoinedAt).HasColumnName("joined_at");
        builder.Property(e => e.LastAccessedAt).HasColumnName("last_accessed_at");
        builder.Property(c => c.Status).HasColumnName("status").HasConversion<byte>().HasColumnType("smallint").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.CompanyId).HasColumnName("company_id");
        builder.HasOne(x => x.User).WithMany(u => u.Companies).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Company).WithMany(c => c.Users).HasForeignKey(x => x.CompanyId);

        builder.HasMany(uc => uc.ModuleRoles)
            .WithOne()
            .HasForeignKey(x => x.UserCompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.UserId, x.CompanyId }).IsUnique();

    }
}
