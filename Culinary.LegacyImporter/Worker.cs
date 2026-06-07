using Nexora.Infrastructure.Persistence;

namespace Culinary.LegacyImporter;

public sealed class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Worker> _logger;

    private static readonly Guid LegacyCompanyId = Guid.Parse("f75d0fea-1ea0-4cb2-b4ac-1fadcb03797f");
    private static readonly Guid CurrentCompanyId = Guid.Parse("313cc45f-987b-4296-9f99-c2c3c3dd0720");


    public Worker(
        IServiceProvider serviceProvider,
        ILogger<Worker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var legacy = scope.ServiceProvider
            .GetRequiredService<LegacyDbContext>();

        var current = scope.ServiceProvider
            .GetRequiredService<CulinaryDbContext>();

        // IMPORT LEGACY LEVELS
        // var legacyLevels = await legacy.Levels.Where(l => l.Active == Status.Active && l.CompanyId == LegacyCompanyId).ToListAsync();
        // var categories = legacyLevels.Select(level =>
        // {
        //     var categoryName = new CategoryName(level.Name);
        //     var category = new Category(level.Id, level.Slug, categoryName, string.Empty, null, 0, Status.Active, null, CurrentCompanyId);
        //     return category;
        // });

        // await current.Categories.AddRangeAsync(categories);
        // await current.SaveChangesAsync();

        // IMPORT LEGACY CATEGORIES
        // var legacyCategory = await legacy.Categories.Where(c => c.Active == Status.Active && c.CompanyId == LegacyCompanyId).ToListAsync();
        // var categories = legacyCategory.Select(legacyCategory =>
        // {
        //     var categoryName = new CategoryName(legacyCategory.Name);
        //     var category = new Category(legacyCategory.Id, legacyCategory.Slug, categoryName, string.Empty, legacyCategory.LevelId, 0, Status.Active, null, CurrentCompanyId);
        //     return category;
        // });

        // await current.Categories.AddRangeAsync(categories);
        // await current.SaveChangesAsync();

        // IMPORT LEGACY RECIPES
        // var legacyRecipe = await legacy.Recipes.Include(r => r.Images).Where(c => c.Active == Status.Active && c.CompanyId == LegacyCompanyId).ToListAsync();
        // var recipes = legacyRecipe.Select(legacyRecipe =>
        // {
        //     var ingredients = new RecipeIngredient(legacyRecipe.Ingredients);
        //     var steps = new RecipeHowToStep(0, legacyRecipe.Preparation);
        //     var recipeContent = new RecipeContent(string.Empty, string.Empty, [ingredients], [steps], string.Empty);
        //     var imageUrl = legacyRecipe.Images.OrderBy(x => x.Id).FirstOrDefault()?.Url;
        //     var recipeMedia = new RecipeMedia(imageUrl ?? string.Empty);
        //     var recipeTiming = new RecipeTiming(0, 0, 0);
        //     var recipeYield = new RecipeYield(string.Empty);
        //     var recipeAttributes = new RecipeAttributes(CulinaryRecipeDifficulty.Medium, string.Empty);
        //     var recipeSeo = new RecipeSeo(string.Empty, string.Empty, $"https://culinary.maisreceitas.com.br/legacy/{legacyRecipe.Slug.Value}");
        //     var structuredDataProvider = new RecipeStructuredDataProvider(
        //             Options.Create(
        //                 new ApiOptions
        //                 {
        //                     BaseUrl = "https://webeditor-node.tudolinux.com.br"
        //                 }));

        //     var recipe = new Recipe(legacyRecipe.Id, legacyRecipe.Slug, legacyRecipe.Name, recipeContent, recipeTiming, recipeYield, recipeAttributes, recipeSeo, legacyRecipe.CategoryId, legacyRecipe.CompanyId);
        //     recipe.SetMedia(recipeMedia);
        //     return recipe;
        // });

        // await current.Recipes.AddRangeAsync(recipes);
        // await current.SaveChangesAsync();

        // IMPORT LEGACY Ratings
        // var legacyRating = await legacy.RecipeRatings.Where(c => c.Active == Status.Active && c.CompanyId == LegacyCompanyId && c.DeletedAt == null).ToListAsync();
        // var ratings = legacyRating.Select(legacyRating =>
        // {
        //     var score = new RecipeScore(legacyRating.Rate);
        //     var rating = new RecipeRating(score, legacyRating.Name, legacyRating.Comment, legacyRating.Active, legacyRating.RecipeId, CurrentCompanyId);
        //     return rating;
        // });

        // await current.RecipeRatings.AddRangeAsync(ratings);
        // await current.SaveChangesAsync();

        Environment.Exit(0);
    }
}