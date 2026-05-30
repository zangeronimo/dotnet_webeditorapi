namespace Nexora.Application.Requests.UseCases.System.UserCompanies;

public sealed record UpdateUserCompanyAvatarRequest(
Guid UserCompanyId,
FileData Avatar,
RequestContext Context
) : ApplicationRequest(Context);
