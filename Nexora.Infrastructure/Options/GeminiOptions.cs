namespace Nexora.Infrastructure.Options;

public class GeminiOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string Model { get; set; } = "gemini-flash-latest";
    public string ModelRetry { get; set; } = "gemini-3.5-flash-late";
    public double Temperature { get; set; } = 0.2;
    public int MaxOutputTokens { get; set; } = 8192;
}