using Nexora.Application.Requests;

namespace Nexora.Application.Interfaces;

public interface IStorageProvider
{
    Task<string> SaveFileAsync(string file, string company, string? prefix = null);
    Task<string> SaveStreamAsync(FileData fileData, string company, string? prefix = null);
    Task DeleteFileAsync(string file);
}
