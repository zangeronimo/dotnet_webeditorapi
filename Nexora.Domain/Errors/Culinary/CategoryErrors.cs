namespace Nexora.Domain.Errors.Culinary;

public static class CategoryErrors
{
    public const string NotFound = "category_not_found";
    public const string AlreadyExists = "category_already_exists";
    public const string NameRequired = "category_name_required";
    public const string CantBeDeletedWithChildren = "category_cannot_be_deleted_with_children";
}

