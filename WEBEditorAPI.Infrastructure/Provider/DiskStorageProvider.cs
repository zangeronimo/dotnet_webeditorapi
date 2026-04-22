using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using WEBEditorAPI.Domain.Interfaces.Provider;

namespace WEBEditorAPI.Infrastructure.Provider;

public class DiskStorageProvider : IStorageProvider
{
    private readonly string _basePath;

    public DiskStorageProvider(IWebHostEnvironment env)
    {
        _basePath = Path.Combine(env.ContentRootPath, "upload");
    }

    public async Task<string> SaveFileAsync(string file, string company, string? prefix = null)
    {
        if (string.IsNullOrWhiteSpace(file))
            return string.Empty;

        // separa header e base64
        var parts = file.Split(',');
        if (parts.Length != 2)
            throw new Exception("Invalid base64 file format");

        var header = parts[0];
        var base64Data = parts[1];

        // extrai tipo (ex: image/png)
        var match = Regex.Match(header, @"data:(.*?);base64");
        if (!match.Success)
            throw new Exception("Invalid file header");

        var mimeType = match.Groups[1].Value;
        var extension = mimeType.Split('/').Last();

        var fileName = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.{extension}";

        var folder = string.IsNullOrEmpty(prefix)
            ? company
            : Path.Combine(company, prefix);

        var dir = Path.Combine(_basePath, folder);

        CreateDir(dir);

        var filePath = Path.Combine(dir, fileName);

        var bytes = Convert.FromBase64String(base64Data);

        await File.WriteAllBytesAsync(filePath, bytes);

        return $"/files/{folder.Replace("\\", "/")}/{fileName}";
    }

    public async Task DeleteFileAsync(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
            return;

        try
        {
            var relativePath = file.Replace("/files/", "upload/");
            var fullPath = Path.Combine(_basePath, relativePath.Replace("upload/", ""));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        catch
        {
            // mesma lógica silenciosa do Node
            return;
        }

        await Task.CompletedTask;
    }

    private static void CreateDir(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }
}
