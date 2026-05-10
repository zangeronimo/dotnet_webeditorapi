namespace WEBEditorAPI.Domain.Errors.Core;

public static class UserErrors
{
    public const string NotFound = "user.not_found";
    public const string AlreadyExists = "user.already_exists";
    public const string EmailAlreadyRegistered = "user.email_already_registered";
    public const string DeleteOwnAccountNotAllowed = "user.delete_own_account_not_allowed";
}