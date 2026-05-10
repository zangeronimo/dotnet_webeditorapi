namespace WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;

public sealed record GetUserCompanyModulesRequest(Guid ResourceId, RequestContext Context) : ApplicationRequest(Context);
