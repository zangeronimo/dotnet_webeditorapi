using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.ValueObjects;
using WEBEditorAPI.Domain.ValueObjects.Culinary;

namespace WEBEditorAPI.Domain.Entities.Culinary;

public class Recipe : Entity
{
    public Slug Slug { get; private set; }
    public string Name { get; private set; }
    public RecipeContent Content { get; private set; }
    public RecipeTiming Timing { get; private set; }
    public RecipeYield Yield { get; private set; }
    public RecipeAttributes Attributes { get; private set; }
    public RecipeMedia Media { get; private set; }
    public RecipeSeo Seo { get; private set; }
    public string? SchemaJsonLd { get; private set; } = null;
    public RecipeEngagement Engagement { get; private set; }
    public Status Active { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public Guid LevelId { get; private set; }
    public Guid CompanyId { get; private set; }

    public Recipe(
        Slug slug,
        string name,
        RecipeContent content,
        RecipeTiming timing,
        RecipeYield yield,
        RecipeAttributes attributes,
        RecipeMedia media,
        RecipeSeo seo,
        RecipeEngagement engagement,
        Status active,
        Guid levelId,
        Guid companyId) : base()
    {
        Slug = slug;
        Name = name;
        Content = content;
        Timing = timing;
        Yield = yield;
        Attributes = attributes;
        Media = media;
        Seo = seo;
        Engagement = engagement;
        Active = active;
        LevelId = levelId;
        CompanyId = companyId;
    }

    protected Recipe() : base() { }

    public void Update(
        Slug newSlug,
        string newName,
        RecipeContent newContent,
        RecipeTiming newTiming,
        RecipeYield newYield,
        RecipeAttributes newAttributes,
        RecipeMedia newMedia,
        RecipeSeo newSeo,
        RecipeEngagement newEngagement,
        Status newActive)
    {
        Slug = newSlug;
        Name = newName;
        Content = newContent;
        Timing = newTiming;
        Yield = newYield;
        Attributes = newAttributes;
        Media = newMedia;
        Seo = newSeo;
        Engagement = newEngagement;
        Active = newActive;
        Touch();
    }
}
