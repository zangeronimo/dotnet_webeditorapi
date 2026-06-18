namespace Nexora.Application.Requests.UseCases.System.ApiClients;

public sealed record GenerateApiClientRequest(Guid Id, RequestContext Context) : ApplicationRequest(Context);