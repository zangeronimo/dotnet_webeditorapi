namespace WEBEditorAPI.Application.Interfaces;

public interface IStorageProvider
{
    Task<string> SaveFileAsync(string file, string company, string? prefix = null);
    Task DeleteFileAsync(string file);
}
