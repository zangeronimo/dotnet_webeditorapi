namespace Nexora.Application.Requests.UseCases.System.ApiClients;

public sealed record InactivateApiClientRequest(Guid Id, RequestContext Context) : ApplicationRequest(Context);