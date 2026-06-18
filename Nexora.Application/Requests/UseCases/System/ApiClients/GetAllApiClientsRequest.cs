namespace Nexora.Application.Requests.UseCases.System.ApiClients;

public sealed record GetAllApiClientsRequest(RequestContext Context) : ApplicationRequest(Context);