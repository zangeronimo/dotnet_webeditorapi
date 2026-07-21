namespace Nexora.Application.Requests.UseCases.AI;

public sealed record GenerateContentRequest(
    string Prompt,
    RequestContext Context
) : ApplicationRequest(Context);
