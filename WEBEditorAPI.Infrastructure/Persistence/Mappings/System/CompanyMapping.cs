using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.System;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.System;


public class CompanyMapping : EntityMapping<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);
        builder.ToTable("webeditor_companies");

        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(150).IsRequired();

        // Many-to-Many unidirectional relationship
        builder.HasMany(c => c.Modules)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "webeditor_companies_has_webeditor_modules",
                j => j.HasOne<Module>()
                      .WithMany()
                      .HasForeignKey("webeditor_modules_id")
                      .HasConstraintName("FK_8808e632be25cecca8d0807f954"),
                j => j.HasOne<Company>()
                      .WithMany()
                      .HasForeignKey("webeditor_companies_id")
                      .HasConstraintName("FK_bf1fdb2d60e99b731bfd9c0de30"),
                j =>
                {
                    j.HasKey("webeditor_companies_id", "webeditor_modules_id");
                });
    }
}
