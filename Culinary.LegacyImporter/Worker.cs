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
        // var legacyRecipe = await legacy.Recipes.Include(r => r.Images).Where(c => c.Active == Nexora.Domain.Enums.Status.Active && c.Imported == 0 && c.CompanyId == LegacyCompanyId).ToListAsync();

        // var recipes = legacyRecipe.Select(legacyRecipe =>
        // {
        //     var ingredients = new RecipeIngredient(legacyRecipe.Ingredients);
        //     var steps = new RecipeHowToStep(0, legacyRecipe.Preparation);
        //     var section = new RecipeSection("", [ingredients], [steps]);
        //     var recipeContent = new RecipeContent(string.Empty, string.Empty, [section], []);
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

        //     var recipe = new Recipe(legacyRecipe.Id, legacyRecipe.Slug, legacyRecipe.Name, recipeContent, recipeTiming, recipeYield, recipeAttributes, recipeSeo, legacyRecipe.CategoryId, CurrentCompanyId);
        //     recipe.SetMedia(recipeMedia);
        //     return recipe;
        // });

        // await current.Recipes.AddRangeAsync(recipes);
        // await current.SaveChangesAsync();

        // IMPORT CULINARY RECIPES
        // var culinaryRecipes = await legacy.CulinaryRecipes.Where(c =>
        //     c.DeletedAt == null &&
        //     c.CompanyId == LegacyCompanyId).ToListAsync();

        // var recipes = culinaryRecipes.Select(culinaryRecipes =>
        // {
        //     var ingredients = new RecipeIngredient(culinaryRecipes.Ingredients);
        //     var steps = new RecipeHowToStep(0, culinaryRecipes.Preparation);
        //     var section = new RecipeSection("", [ingredients], [steps]);
        //     var recipeContent = new RecipeContent(
        //         culinaryRecipes.ShortDescription,
        //         culinaryRecipes.FullDescription,
        //         [section], [culinaryRecipes.Notes]);
        //     var recipeMedia = new RecipeMedia(culinaryRecipes.ImageUrl ?? string.Empty);
        //     var recipeTiming = new RecipeTiming(culinaryRecipes.PrepTime, culinaryRecipes.CookTime, culinaryRecipes.RestTime);
        //     var recipeYield = new RecipeYield(culinaryRecipes.YieldTotal);
        //     var difficulty = culinaryRecipes.Difficulty == "Fácil" ? CulinaryRecipeDifficulty.Easy : culinaryRecipes.Difficulty == "Média" ? CulinaryRecipeDifficulty.Medium : CulinaryRecipeDifficulty.Hard;
        //     var recipeAttributes = new RecipeAttributes(difficulty, culinaryRecipes.Cuisine);
        //     var recipeSeo = new RecipeSeo(culinaryRecipes.MetaTitle?.Substring(0, Math.Min(70, culinaryRecipes.MetaTitle.Length)), culinaryRecipes.MetaDescription, $"https://culinaria.maisreceitas.com.br/recipe/{culinaryRecipes.Slug.Value}");
        //     var structuredDataProvider = new RecipeStructuredDataProvider(
        //             Options.Create(
        //                 new ApiOptions
        //                 {
        //                     BaseUrl = "https://webeditor-node.tudolinux.com.br"
        //                 }));

        //     var recipe = new Recipe(culinaryRecipes.Id, Slug.Restore(culinaryRecipes.Slug.Value), culinaryRecipes.Name, recipeContent, recipeTiming, recipeYield, recipeAttributes, recipeSeo, culinaryRecipes.CategoryId, CurrentCompanyId);
        //     recipe.SetMedia(recipeMedia);
        //     return recipe;
        // });

        // await current.Recipes.AddRangeAsync(recipes);
        // await current.SaveChangesAsync();

        Environment.Exit(0);
    }
}