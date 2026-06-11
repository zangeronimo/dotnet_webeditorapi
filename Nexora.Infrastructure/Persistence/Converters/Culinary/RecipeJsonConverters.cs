using System.Text.Json;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Nexora.Domain.ValueObjects.Culinary;

namespace Nexora.Infrastructure.Persistence.Converters.Culinary;

public static class RecipeJsonConverters
{
    public static readonly ValueConverter<IReadOnlyCollection<RecipeSection>, string>
        SectionsConverter =
            new(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<RecipeSection>>(v, (JsonSerializerOptions?)null)!
            );

    public static readonly ValueConverter<IReadOnlyCollection<string>, string>
        NotesConverter =
            new(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null)!
            );
}