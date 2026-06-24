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
        var baseUrl = _options.BaseUrl?.TrimEnd('/');

        var dto = new RecipeStructuredDataDto
        {
            Name = recipe.Name,

            Description = recipe.Content.FullDescription,

            Image = string.IsNullOrWhiteSpace(recipe.Media.ImageUrl)
                ? null
                : new[] { NormalizeUrl(recipe.Media.ImageUrl, baseUrl) },

            Author = new AuthorDto
            {
                Name = "Nexora Culinary"
            },

            DatePublished = recipe.PublishedAt?.ToString("yyyy-MM-dd"),

            PrepTime = ToIso8601Duration(recipe.Timing.PrepTime),

            CookTime = ToIso8601Duration(recipe.Timing.CookTime),

            TotalTime = ToIso8601Duration(
                recipe.Timing.PrepTime +
                recipe.Timing.CookTime +
                recipe.Timing.RestTime),

            RecipeYield = recipe.Yield.YieldTotal,

            RecipeCategory = category.Parent?.Name?.Value ?? category.Name.Value,

            RecipeCuisine = recipe.Attributes.Cuisine,

            Keywords = BuildKeywords(category, tags, recipe),

            RecipeIngredient = recipe.Content.Sections
                .SelectMany(s => s.Ingredients)
                .Select(i => i.Description)
                .ToList(),

            RecipeInstructions = recipe.Content.Sections
                .SelectMany(s => s.Steps)
                .Select(s => new HowToStepDto
                {
                    Text = s.Instruction
                })
                .ToList()
        };

        return JsonSerializer.Serialize(dto, new JsonSerializerOptions
        {
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });
    }

    private static string NormalizeUrl(string url, string baseUrl)
    {
        if (string.IsNullOrWhiteSpace(url))
            return null;

        if (url.StartsWith("http"))
            return url;

        return $"{baseUrl}{url}";
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
        var keywords = new List<string>
        {
            category.Name.Value,
            recipe.Attributes.Difficulty.ToString(),
            recipe.Name
        };

        if (!string.IsNullOrWhiteSpace(recipe.Attributes.Cuisine))
            keywords.Add(recipe.Attributes.Cuisine);

        keywords.AddRange(
            tags
                .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                .Select(x => x.Name));

        return string.Join(
            ", ",
            keywords.Distinct(StringComparer.OrdinalIgnoreCase));
    }
}