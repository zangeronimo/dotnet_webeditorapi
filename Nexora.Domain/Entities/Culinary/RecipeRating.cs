using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Domain.Entities.Culinary;

public class RecipeRating : Entity
{
    public RecipeScore Score { get; private set; }
    public string? Name { get; private set; }
    public string? Comment { get; private set; }
    public Status Status { get; private set; }
    public DateTimeOffset? PublishedAt { get; private set; }
    public Guid RecipeId { get; private set; }
    public Guid CompanyId { get; private set; }

    public RecipeRating(
     RecipeScore score,
     string? name,
     string? comment,
     Status status,
     Guid recipeId,
     Guid companyId) : base()
    {
        Score = score;
        Name = name;
        Comment = comment;
        Status = status;
        RecipeId = recipeId;
        CompanyId = companyId;

        PublishIfNeeded();
    }

    protected RecipeRating() : base() { }

    public void Update(
        RecipeScore newScore,
        string? newName,
        string? newComment,
        Status newStatus)
    {
        Score = newScore;
        Name = newName;
        Comment = newComment;
        Status = newStatus;

        PublishIfNeeded();
        Touch();
    }

    private void PublishIfNeeded()
    {
        if (PublishedAt is null &&
            Status == Status.Active)
        {
            PublishedAt = DateTimeOffset.UtcNow;
        }
    }
}