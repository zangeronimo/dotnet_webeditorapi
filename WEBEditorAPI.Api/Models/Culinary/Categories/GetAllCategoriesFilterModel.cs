namespace WEBEditorAPI.Api.Models.Culinary.Categories;

public class GetAllCategoriesFilterModel : PaginationModel
{
    public string OrderBy { get; init; } = "Id";
    public bool Desc { get; init; } = false;
    public string? Name { get; init; }
    public int? Active { get; init; }
}
