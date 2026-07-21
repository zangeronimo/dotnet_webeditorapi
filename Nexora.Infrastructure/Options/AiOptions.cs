namespace Nexora.Infrastructure.Options;

public class AiOptions
{
    public const string SectionName = "AI";

    public string Provider { get; set; } = "Gemini";

    public GeminiOptions Gemini { get; set; } = new();
}
