using Nexora.Application.Requests;

namespace Nexora.Application.Interfaces;

public interface IUseCase<TRequest, TResponse> where TRequest : ApplicationRequest
{
    Task<TResponse> ExecuteAsync(TRequest request);
}
