namespace WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;

public sealed record UpdateUserCompanyAvatarRequest(
Guid UserCompanyId,
FileData Avatar,
RequestContext Context
) : ApplicationRequest(Context);
