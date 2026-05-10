namespace WEBEditorAPI.Application.Requests.UseCases.Core.Users;

public sealed record UserProfileAvatarRequest(
FileData Avatar,
RequestContext Context
) : ApplicationRequest(Context);
