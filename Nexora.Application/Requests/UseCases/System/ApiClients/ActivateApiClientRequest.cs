namespace Nexora.Application.Requests.UseCases.System.ApiClients;

public sealed record ActivateApiClientRequest(Guid Id, RequestContext Context) : ApplicationRequest(Context);