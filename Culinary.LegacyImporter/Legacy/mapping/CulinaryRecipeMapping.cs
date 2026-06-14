using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Nexora.Domain.Entities.Culinary;
using Nexora.Domain.ValueObjects;

namespace Culinary.LegacyImporter.Legacy.mapping;

public class CulinaryRecipeMapping : IEntityTypeConfiguration<CulinaryRecipe>
{
    public void Configure(EntityTypeBuilder<CulinaryRecipe> builder)
    {
        builder.ToTable("recipe_recipes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(c => c.Slug)
            .HasConversion(v => v.Value, v => Slug.Create(v))
            .HasColumnName("slug")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name");

        builder.Property(x => x.ShortDescription)
            .HasColumnName("short_description");

        builder.Property(x => x.FullDescription)
            .HasColumnName("full_description");

        builder.Property(x => x.Ingredients)
            .HasColumnName("ingredients");

        builder.Property(x => x.Preparation)
            .HasColumnName("preparation");

        builder.Property(x => x.YieldTotal).HasColumnName("yield_total");
        builder.Property(x => x.PrepTime).HasColumnName("prep_time");
        builder.Property(x => x.CookTime).HasColumnName("cook_time");
        builder.Property(x => x.RestTime).HasColumnName("rest_time");
        builder.Property(x => x.Difficulty).HasColumnName("difficulty");
        builder.Property(x => x.Notes).HasColumnName("notes");
        builder.Property(r => r.Cuisine).HasColumnName("cuisine");
        builder.Property(x => x.ImageUrl).HasColumnName("image_url");
        builder.Property(x => x.MetaTitle).HasColumnName("meta_title");
        builder.Property(x => x.MetaDescription).HasColumnName("meta_description");

        builder.Property(r => r.PublishedAt).HasColumnName("published_at");
        builder.Property(r => r.DeletedAt).HasColumnName("deleted_at");

        builder.Property(c => c.Active).HasColumnName("active").HasConversion<int>().IsRequired();

        builder.Property(c => c.CategoryId).HasColumnName("recipe_levels_id");
        builder.Property(c => c.CompanyId).HasColumnName("webeditor_companies_id");

    }
}

