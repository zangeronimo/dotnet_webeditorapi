using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.AI;

namespace Nexora.Application.UseCases.Culinary.Recipes;

public class GenerateContentUC(IAiProvider aiProvider) : IUseCase<GenerateContentRequest, string>
{
    private readonly IAiProvider _aiProvider = aiProvider;
    public async Task<string> ExecuteAsync(GenerateContentRequest request)
    {
        return await _aiProvider.GenerateAsync(request.Prompt);
    }
}
