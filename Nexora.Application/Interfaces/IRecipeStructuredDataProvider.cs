using Nexora.Domain.Entities.Culinary;

namespace Nexora.Application.Interfaces;

public interface IRecipeStructuredDataProvider
{
    string Generate(
        Recipe recipe,
        Category category,
        IReadOnlyCollection<Tag> tags);
}