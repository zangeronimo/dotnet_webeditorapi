using System.Text.Json;
using Microsoft.Extensions.Options;
using Nexora.Application.Interfaces;
using Nexora.Domain.Entities.Culinary;
using Nexora.Infrastructure.Options;

namespace Nexora.Infrastructure.Provider.StructuredData;

public sealed class RecipeStructuredDataProvider
    : IRecipeStructuredDataProvider
{
    private readonly ApiOptions _options;

    public RecipeStructuredDataProvider(
        IOptions<ApiOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(
        Recipe recipe,
        Category category,
        IReadOnlyCollection<Tag> tags)
    {
        var baseUrl = _options.BaseUrl.TrimEnd('/');

        var structuredData = new
        {
            @context = "https://schema.org",
            @type = "Recipe",

            name = recipe.Name,

            description = recipe.Content.ShortDescription,

            image = string.IsNullOrWhiteSpace(recipe.Media.ImageUrl)
                ? null
                : new[]
                {
                    $"{baseUrl}{recipe.Media.ImageUrl}"
                },

            author = new
            {
                @type = "Organization",
                name = "Nexora Culinary"
            },

            datePublished = recipe.PublishedAt?.ToString("yyyy-MM-dd"),

            prepTime = ToIso8601Duration(
                recipe.Timing.PrepTime),

            cookTime = ToIso8601Duration(
                recipe.Timing.CookTime),

            totalTime = ToIso8601Duration(
                recipe.Timing.PrepTime +
                recipe.Timing.CookTime +
                recipe.Timing.RestTime),

            recipeYield = recipe.Yield.YieldTotal,

            recipeCategory = category.Name.Value,
            recipeCuisine = recipe.Attributes.Cuisine,

            keywords = BuildKeywords(
                category,
                tags,
                recipe),

            recipeIngredient = recipe.Content.Ingredients
                .Select(x => x.Description)
                .ToList(),

            recipeInstructions = recipe.Content.Steps
                .OrderBy(x => x.Order)
                .Select(x => new
                {
                    @type = "HowToStep",
                    text = x.Instruction
                })
                .ToList()
        };

        return JsonSerializer.Serialize(
            structuredData,
            new JsonSerializerOptions
            {
                WriteIndented = false
            });
    }

    private static string? ToIso8601Duration(int minutes)
    {
        if (minutes <= 0)
            return null;

        var hours = minutes / 60;
        var mins = minutes % 60;

        if (hours == 0)
            return $"PT{mins}M";

        if (mins == 0)
            return $"PT{hours}H";

        return $"PT{hours}H{mins}M";
    }

    private static string BuildKeywords(
        Category category,
        IReadOnlyCollection<Tag> tags,
        Recipe recipe)
    {
        var keywords = new List<string>();

        keywords.Add(category.Name.Value);

        if (!string.IsNullOrWhiteSpace(recipe.Attributes.Cuisine))
            keywords.Add(recipe.Attributes.Cuisine);

        keywords.Add(recipe.Attributes.Difficulty.ToString());

        keywords.AddRange(
            tags
                .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                .Select(x => x.Name));

        return string.Join(
            ", ",
            keywords
                .Distinct(StringComparer.OrdinalIgnoreCase));
    }
}