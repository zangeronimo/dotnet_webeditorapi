namespace WEBEditorAPI.Application.Requests.UseCases;

public sealed record GetByIdRequest(Guid ResourceId, RequestContext Context) : ApplicationRequest(Context);
