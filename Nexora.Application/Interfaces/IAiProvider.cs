
namespace Nexora.Application.Interfaces;

public interface IAiProvider
{
    Task<string> GenerateAsync(string prompt, CancellationToken cancellationToken = default);
}