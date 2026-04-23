using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Entities.Culinary;

public class Rating : Entity
{
    public string? Name { get; private set; }
    public int Rate { get; private set; }
    public string? Comment { get; private set; }
    public Guid CompanyId { get; private set; }
    public Status Active { get; private set; }
    public Guid RecipeId { get; private set; }

    public Rating(string? name, int rate, string? comment, Status active, Guid recipeId, Guid companyId) : base()
    {
        Name = name;
        Rate = rate;
        Comment = comment;
        Active = active;
        RecipeId = recipeId;
        CompanyId = companyId;
    }

    protected Rating() : base() { }

    public void Update(string? newName, int newRate, string? newComment, Status newActive)
    {
        Name = newName;
        Rate = newRate;
        Comment = newComment;
        Active = newActive;
        Touch();
    }
}
