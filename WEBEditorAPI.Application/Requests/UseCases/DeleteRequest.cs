namespace WEBEditorAPI.Application.Requests.UseCases;

public sealed record DeleteRequest(Guid ResourceId, RequestContext Context) : ApplicationRequest(Context);
