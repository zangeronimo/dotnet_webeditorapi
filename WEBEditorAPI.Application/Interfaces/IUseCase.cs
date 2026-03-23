namespace WEBEditorAPI.Application.Interfaces;

public interface IUseCase<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
}
