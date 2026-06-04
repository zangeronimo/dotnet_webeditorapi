using Nexora.Domain.Enums;
using Nexora.Domain.Errors.Culiarny;
using Nexora.Domain.Exceptions;
using Nexora.Domain.ValueObjects;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Domain.Entities.Culinary;

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
    public string? StructuredData { get; private set; } = null;
    private readonly List<Guid> _tagIds = [];
    public IReadOnlyCollection<Guid> TagIds => _tagIds;
    public Status Status { get; private set; }
    public decimal AverageRating { get; private set; }
    public int TotalRatings { get; private set; }
    public DateTimeOffset? PublishedAt { get; private set; }
    public Guid CategoryId { get; private set; }
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
        IEnumerable<Guid> tagIds,
        Guid categoryId,
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
        Status = Status.Inactive;
        AverageRating = 0;
        TotalRatings = 0;
        CategoryId = categoryId;
        CompanyId = companyId;

        if (tagIds is null) throw new DomainException(RecipeErrors.InvalidTagIds);
        _tagIds.AddRange(tagIds.Distinct());
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
        Status newStatus,
        Guid newCategoryId)
    {
        Slug = newSlug;
        Name = newName;
        Content = newContent;
        Timing = newTiming;
        Yield = newYield;
        Attributes = newAttributes;
        Media = newMedia;
        Seo = newSeo;
        Status = newStatus;
        CategoryId = newCategoryId;

        PublishIfNeeded();
        Touch();
    }

    public void UpdateRatingSummary(decimal averageRating, int totalRatings)
    {
        if (averageRating < 0 || averageRating > 10)
            throw new DomainException(RecipeErrors.InvalidAverageRating);

        if (totalRatings < 0)
            throw new DomainException(RecipeErrors.InvalidTotalRatings);

        AverageRating = Math.Round(averageRating, 1);
        TotalRatings = totalRatings;

        Touch();
    }

    public void SetTags(IEnumerable<Guid> tagIds)
    {
        if (tagIds is null) throw new DomainException(RecipeErrors.InvalidTagIds);

        _tagIds.Clear();
        _tagIds.AddRange(tagIds.Distinct());
        Touch();
    }

    public void SetStructuredData(string data)
    {

        if (string.IsNullOrWhiteSpace(data))
            throw new DomainException(RecipeErrors.InvalidStructuredData);

        StructuredData = data.Trim();
        Touch();
    }

    private void PublishIfNeeded()
    {
        if (PublishedAt is null &&
            Status == Status.Active)
        {
            PublishedAt = DateTimeOffset.UtcNow;
        }
    }
}
