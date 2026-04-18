using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEBEditorAPI.Domain.Entities.Culinary;

namespace WEBEditorAPI.Infrastructure.Persistence.Mappings.Culinary;

public class RecipeMapping : EntityMapping<Recipe>
{
    public override void Configure(EntityTypeBuilder<Recipe> builder)
    {
        base.Configure(builder);
        builder.ToTable("recipe_recipes");

        builder.OwnsOne(r => r.Slug, slug =>
        {
            slug.Property(s => s.Value).HasColumnName("slug").HasMaxLength(120).IsRequired();
        });
        builder.Property(r => r.Name).HasColumnName("name").HasMaxLength(120).IsRequired();
        builder.OwnsOne(r => r.Content, content =>
        {
            content.Property(p => p.ShortDescription).HasColumnName("short_description").HasMaxLength(255);
            content.Property(p => p.FullDescription).HasColumnName("full_description");
            content.Property(p => p.Ingredients).HasColumnName("ingredients").IsRequired();
            content.Property(p => p.Preparation).HasColumnName("preparation").IsRequired();
            content.Property(p => p.Notes).HasColumnName("notes");
        });
        builder.OwnsOne(r => r.Attributes, attribute =>
        {
            attribute.Property(p => p.Difficulty).HasColumnName("difficulty").HasMaxLength(20);
            attribute.Property(p => p.Tools).HasColumnName("tools");
            attribute.Property(p => p.Cuisine).HasColumnName("cuisine").HasMaxLength(100);
        });
        builder.OwnsOne(r => r.Engagement, engagement =>
        {
            engagement.Property(p => p.Views).HasColumnName("views").HasConversion<int>().IsRequired();
            engagement.Property(p => p.Likes).HasColumnName("likes").HasConversion<int>().IsRequired();
        });
        builder.OwnsOne(r => r.Yield, yield =>
        {
            yield.Property(y => y.YieldTotal).HasColumnName("yield_total").HasMaxLength(50);
        });
        builder.OwnsOne(r => r.Timing, timing =>
        {
            timing.Property(t => t.PrepTime).HasColumnName("prep_time").HasConversion<int>();
            timing.Property(t => t.CookTime).HasColumnName("cook_time").HasConversion<int>();
            timing.Property(t => t.RestTime).HasColumnName("rest_time").HasConversion<int>();
        });
        builder.OwnsOne(r => r.Seo, seo =>
        {
            seo.Property(s => s.MetaTitle).HasColumnName("meta_title").HasMaxLength(255);
            seo.Property(s => s.MetaDescription).HasColumnName("meta_description").HasMaxLength(255);
            seo.Property(s => s.Keywords).HasColumnName("keywords").HasColumnType("varchar[]");
        });
        builder.OwnsOne(r => r.Media, media =>
        {
            media.Property(m => m.ImageUrl).HasColumnName("image_url").HasMaxLength(255);
        });
        builder.Property(r => r.SchemaJsonLd).HasColumnName("schema_jsonld").HasColumnType("jsonb");
        builder.Property(r => r.Active).HasColumnName("active").HasConversion<int>().IsRequired();
        builder.Property(r => r.LevelId).HasColumnName("recipe_levels_id").IsRequired();
        builder.Property(r => r.CompanyId).HasColumnName("webeditor_companies_id").IsRequired();
        builder.Property(r => r.PublishedAt).HasColumnName("published_at");
    }
}