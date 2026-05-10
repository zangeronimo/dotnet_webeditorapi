using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;

namespace WEBEditorAPI.Infrastructure.Provider;

public class DiskStorageProvider : IStorageProvider
{
    private readonly string _basePath;

    public DiskStorageProvider(IWebHostEnvironment env)
    {
        _basePath = Path.Combine(env.ContentRootPath, "upload");
    }

    public async Task<string> SaveFileAsync(
        string file,
        string company,
        string? prefix = null)
    {
        if (string.IsNullOrWhiteSpace(file))
            return string.Empty;

        var parts = file.Split(',');

        if (parts.Length != 2)
            throw new Exception("Invalid base64 file format");

        var header = parts[0];
        var base64Data = parts[1];

        var match = Regex.Match(header, @"data:(.*?);base64");

        if (!match.Success)
            throw new Exception("Invalid file header");

        var mimeType = match.Groups[1].Value;

        var extension = GetExtension(mimeType);

        var (filePath, publicPath) = BuildPaths(
            company,
            extension,
            prefix);

        var bytes = Convert.FromBase64String(base64Data);

        await File.WriteAllBytesAsync(filePath, bytes);

        return publicPath;
    }

    public async Task<string> SaveStreamAsync(
        FileData fileData,
        string company,
        string? prefix = null)
    {
        if (fileData.Stream == null || !fileData.Stream.CanRead)
            throw new Exception("Invalid stream");

        if (fileData.Stream.CanSeek)
        {
            fileData.Stream.Position = 0;
        }

        var extension = GetExtension(fileData.ContentType);

        var (filePath, publicPath) = BuildPaths(
            company,
            extension,
            prefix);

        await using var fileStream = new FileStream(
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None);

        await fileData.Stream.CopyToAsync(fileStream);

        return publicPath;
    }

    public async Task DeleteFileAsync(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
            return;

        try
        {
            var relativePath = file.Replace("/files/", "");

            var fullPath = Path.Combine(_basePath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        catch
        {
            return;
        }

        await Task.CompletedTask;
    }

    private (string FilePath, string PublicPath) BuildPaths(
        string company,
        string extension,
        string? prefix)
    {
        var fileName = $"{Guid.NewGuid()}.{extension}";

        var folder = string.IsNullOrWhiteSpace(prefix)
            ? company
            : Path.Combine(company, prefix);

        var dir = Path.Combine(_basePath, folder);

        CreateDir(dir);

        var filePath = Path.Combine(dir, fileName);

        var publicPath =
            $"/files/{folder.Replace("\\", "/")}/{fileName}";

        return (filePath, publicPath);
    }

    private static void CreateDir(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    private static string GetExtension(string contentType)
    {
        return contentType.ToLower() switch
        {
            "image/webp" => "webp",
            _ => throw new Exception("Unsupported file type")
        };
    }
}