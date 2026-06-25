using Nexora.Domain.Enums;

namespace Nexora.Api.Models.Culinary.Recipes;

public class GetAllRecipesFilterModel : PaginationModel
{
    public string OrderBy { get; init; } = "Status";
    public bool Desc { get; init; } = false;
    public string? Name { get; init; }
    public Status? Status { get; init; }
    public Guid? CategoryId { get; set; }
}
