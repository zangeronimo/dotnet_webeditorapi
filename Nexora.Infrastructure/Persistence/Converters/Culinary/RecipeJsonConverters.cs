using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Infrastructure.Persistence.Converters.Culinary;

public static class RecipeJsonConverters
{
    public static readonly ValueConverter<IReadOnlyCollection<RecipeIngredient>, string>
        IngredientsConverter =
            new(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<RecipeIngredient>>(v, (JsonSerializerOptions?)null)!
            );

    public static readonly ValueConverter<IReadOnlyCollection<RecipeHowToStep>, string>
        StepsConverter =
            new(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<RecipeHowToStep>>(v, (JsonSerializerOptions?)null)!
            );
}