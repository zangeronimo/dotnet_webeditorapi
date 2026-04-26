using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Core;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Core;

public class UserCompanyModuleRoleMapping : IEntityTypeConfiguration<UserCompanyModuleRole>
{
    public void Configure(EntityTypeBuilder<UserCompanyModuleRole> builder)
    {
        builder.ToTable("core_user_company_roles");

        builder.HasKey(x => new { x.UserCompanyId, x.ModuleId });

        builder.Property(x => x.UserCompanyId).HasColumnName("user_company_id");
        builder.Property(x => x.ModuleId).HasColumnName("module_id");
        builder.Property(x => x.RoleId).HasColumnName("role_id");

        builder.HasOne<UserCompany>()
            .WithMany(uc => uc.ModuleRoles)
            .HasForeignKey(x => x.UserCompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Module>()
            .WithMany()
            .HasForeignKey(x => x.ModuleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}