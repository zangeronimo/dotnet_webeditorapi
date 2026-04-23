using WEBEditorAPI.Domain.Entities.Culinary;

namespace WEBEditorAPI.Application.Requests.JsonLd;

public sealed record RecipeJsonLdRequest(Recipe Recipe, Level Level) { }