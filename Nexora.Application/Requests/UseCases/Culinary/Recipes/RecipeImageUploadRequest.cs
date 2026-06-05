namespace Nexora.Application.Requests.UseCases.Culinary.Recipes;

public sealed record RecipeImageUploadRequest(
Guid RecipeId,
FileData Image,
RequestContext Context
) : ApplicationRequest(Context);