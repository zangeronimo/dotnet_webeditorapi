namespace Nexora.Domain.Errors.System;

public static class UserCompanyErrors
{
    public const string NotFound = "usercompany.not_found";
    public const string AlreadyExists = "usercompany.already_exists";
    public const string DeleteOwnAccountNotAllowed = "usercompany.delete_own_account_not_allowed";
}
