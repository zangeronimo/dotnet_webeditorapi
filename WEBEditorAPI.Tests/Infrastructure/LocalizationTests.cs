using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WEBEditorAPI.Domain;
using WEBEditorAPI.Domain.Config;
using WEBEditorAPI.Domain.Interfaces.Provider;
using WEBEditorAPI.Infrastructure.Provider;

namespace WEBEditorAPI.Tests.Infrastructure;

public class LocalizationTests
{
    private readonly IMessageProvider _messages;

    public LocalizationTests()
    {
        var services = new ServiceCollection();

        services.AddLogging();
        services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

        services.AddTransient<IMessageProvider, MessageProvider>();

        var provider = services.BuildServiceProvider();

        _messages = provider.GetRequiredService<IMessageProvider>();
    }

    [Fact]
    public void All_ErrorKeys_Should_Have_Translations()
    {
        var supportedCultures = LocalizationConfig.SupportedCultures;
        var domainAssembly = typeof(DomainAssemblyMarker).Assembly;

        var errorClasses = domainAssembly.GetTypes()
            .Where(t =>
                t.IsClass &&
                t.IsAbstract &&
                t.IsSealed &&
                t.Namespace != null &&
                t.Namespace.Contains("Errors"));

        var keys = errorClasses
            .SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static))
            .Where(f => f.FieldType == typeof(string))
            .Select(f => f.GetValue(null)?.ToString())
            .Where(k => !string.IsNullOrWhiteSpace(k))
            .Distinct()
            .ToList();

        var missing = new List<string>();

        foreach (var culture in supportedCultures)
        {
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
            foreach (var key in keys)
            {
                var value = _messages.Get(key!);

                if (value == key) // fallback = não encontrou
                {
                    missing.Add($"{culture}: {key}");
                }
            }
        }

        Assert.True(
            missing.Count == 0,
            $"Missing translations:\n{string.Join("\n", missing)}"
        );
    }
}