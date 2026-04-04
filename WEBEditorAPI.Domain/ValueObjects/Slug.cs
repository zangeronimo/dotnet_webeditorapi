using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WEBEditorAPI.Domain.ValueObjects;

public class Slug
{
    public string Value { get; private set; }

    private Slug(string slug)
    {
        Value = slug;
    }

    public static Slug Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Slug name não pode ser vazio.");

        var slug = name.ToLowerInvariant();

        // remove acentos
        slug = RemoveDiacritics(slug);

        // troca espaços por hífen
        slug = slug.Replace(" ", "-");

        // remove caracteres inválidos
        slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");

        // evita múltiplos hífens
        slug = Regex.Replace(slug, @"-+", "-");

        // remove hífen do começo ou final
        slug = slug.Trim('-');

        return new Slug(slug);
    }

    public static Slug Restore(string slug)
    {
        return new Slug(slug);
    }

    private static string RemoveDiacritics(string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            var unicodeCategory = Char.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}
