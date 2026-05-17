using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nexora.Application.DTOs.JsonLd;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.JsonLd;
using Nexora.Domain.Entities.Culinary;
using Nexora.Infrastructure.Options;

namespace Nexora.Infrastructure.Provider.JsonLd;

public class RecipeJsonLdProvider
    : IJsonLdProvider<RecipeJsonLdRequest, RecipeJsonLd>
{
    private readonly IOptions<ApiOptions> _options;

    public RecipeJsonLdProvider(IOptions<ApiOptions> options)
    {
        _options = options;
    }

    public RecipeJsonLd Generate(RecipeJsonLdRequest request)
    {
        var baseUrl = _options.Value.BaseUrl;
        var recipe = request.Recipe;
        var category = request.Category;

        var totalMinutes = recipe.Timing.PrepTime + recipe.Timing.CookTime + recipe.Timing.RestTime;

        return new RecipeJsonLd
        {
            Name = recipe.Name,

            Author = new AuthorJsonLd
            {
                Name = "Mais Receitas"
            },

            Description = recipe.Content.ShortDescription,

            Image = new List<string>
            {
                $"{baseUrl}{recipe.Media.ImageUrl}"
            },

            TotalTime = ToIso8601Duration(totalMinutes),

            PrepTime = recipe.Timing.PrepTime > 0
                ? ToIso8601Duration(recipe.Timing.PrepTime)
                : null,

            CookTime = recipe.Timing.CookTime > 0
                ? ToIso8601Duration(recipe.Timing.CookTime)
                : null,

            RecipeYield = recipe.Yield.YieldTotal,

            RecipeCategory = BuildCategory(recipe, category),

            RecipeCuisine = recipe.Attributes.Cuisine,

            Keywords = string.Join(", ", recipe.Seo.Keywords ?? []),

            RecipeIngredient = SplitLines(RemoveHtml(recipe.Content.Ingredients)),

            RecipeInstructions = BuildInstructions(recipe.Content.Preparation, recipe.Content.Notes),

            Tool = SplitLines(RemoveHtml(recipe.Attributes.Tools)),

            DatePublished = recipe.PublishedAt?.ToString("yyyy-MM-dd")
        };
    }

    // -------------------------
    // Helpers
    // -------------------------

    private static string? ToIso8601Duration(int minutes)
    {
        if (minutes <= 0) return null;

        var days = minutes / 1440;
        var hours = (minutes % 1440) / 60;
        var mins = minutes % 60;

        var duration = "P";

        if (days > 0)
            duration += $"{days}D";

        if (hours > 0 || mins > 0)
        {
            duration += "T";
            if (hours > 0) duration += $"{hours}H";
            if (mins > 0) duration += $"{mins}M";
        }

        return duration;
    }

    private static string RemoveHtml(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return Regex.Replace(input, "<[^>]*>", "").Trim();
    }

    private static List<string> SplitLines(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new List<string>();

        return input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
    }

    private static List<HowToInstructionJsonLd> BuildInstructions(string? preparation, string? notes)
    {
        var list = new List<HowToInstructionJsonLd>();

        // Steps
        if (!string.IsNullOrWhiteSpace(preparation))
        {
            var steps = preparation
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(step => new HowToInstructionJsonLd
                {
                    Type = "HowToStep",
                    Text = RemoveHtml(step)
                });

            list.AddRange(steps);
        }

        // Notes → Tips
        if (!string.IsNullOrWhiteSpace(notes))
        {
            var tips = notes
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(note => new HowToInstructionJsonLd
                {
                    Type = "HowToTip",
                    Text = $"Dica: {RemoveHtml(note)}"
                });

            list.AddRange(tips);
        }

        return list;
    }

    private static string BuildCategory(Recipe recipe, Category category)
    {
        var categoryName = category.Name.Value;

        if (!string.IsNullOrWhiteSpace(recipe.Attributes.Difficulty))
        {
            categoryName = string.IsNullOrWhiteSpace(categoryName)
                ? recipe.Attributes.Difficulty
                : $"{categoryName} - {recipe.Attributes.Difficulty}";
        }

        return categoryName;
    }
}