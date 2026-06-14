namespace Nexora.Application.Requests.UseCases.Culinary.Categories;

public sealed record GetByParentIdRequest(
    Guid ParentId,
    RequestContext Context) : ApplicationRequest(Context);
