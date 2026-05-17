using Nexora.Domain.Entities.Culinary;

namespace Nexora.Application.Requests.JsonLd;

public sealed record RecipeJsonLdRequest(Recipe Recipe, Category Category) { }