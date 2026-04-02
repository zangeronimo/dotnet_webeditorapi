using WEBEditorAPI.Application.Requests;

namespace WEBEditorAPI.Application.Interfaces;

public interface IUseCase<TRequest, TResponse> where TRequest : ApplicationRequest
{
    Task<TResponse> ExecuteAsync(TRequest request);
}
