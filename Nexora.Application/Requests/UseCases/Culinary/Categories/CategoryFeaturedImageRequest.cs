namespace Nexora.Application.Requests.UseCases.Culinary.Categories;

public sealed record CategoryFeaturedImageRequest(
Guid CategoryId,
FileData FeaturedImage,
RequestContext Context
) : ApplicationRequest(Context);