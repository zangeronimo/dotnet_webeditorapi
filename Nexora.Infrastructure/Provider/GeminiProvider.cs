using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Options;

using Nexora.Application.Interfaces;
using Nexora.Infrastructure.Options;

namespace Nexora.Infrastructure.Provider;

public sealed class GeminiProvider(
    HttpClient httpClient,
    IOptions<AiOptions> options) : IAiProvider
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly GeminiOptions _options = options.Value.Gemini;

    public async Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prompt);

        var request = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = prompt
                        }
                    }
                }
            },
            generationConfig = new
            {
                temperature = _options.Temperature,
                maxOutputTokens = _options.MaxOutputTokens
            }
        };

        var result = await SendAsync(
            _options.Model,
            request,
            cancellationToken);

        if (!result.Success &&
            !string.IsNullOrWhiteSpace(_options.ModelRetry))
        {
            result = await SendAsync(
                _options.ModelRetry,
                request,
                cancellationToken);
        }

        if (!result.Success)
        {
            throw new InvalidOperationException(
                $"Gemini error ({result.StatusCode}): {result.Content}");
        }

        using var document = JsonDocument.Parse(result.Content);

        if (!document.RootElement.TryGetProperty("candidates", out var candidates) ||
            candidates.GetArrayLength() == 0)
        {
            throw new InvalidOperationException("Gemini returned no candidates.");
        }

        var text = candidates[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        if (string.IsNullOrWhiteSpace(text))
            throw new InvalidOperationException("Gemini returned an empty response.");

        return text;
    }

    private async Task<GeminiResponse> SendAsync(
        string model,
        object request,
        CancellationToken cancellationToken)
    {
        using var response = await _httpClient.PostAsJsonAsync(
            $"models/{model}:generateContent?key={_options.ApiKey}",
            request,
            cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        return new GeminiResponse(
            response.IsSuccessStatusCode,
            response.StatusCode,
            content);
    }

    private sealed record GeminiResponse(
        bool Success,
        System.Net.HttpStatusCode StatusCode,
        string Content);
}