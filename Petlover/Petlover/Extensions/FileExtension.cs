using System.Runtime.CompilerServices;

namespace Petlover.Extensions;

public static class FileExtension
{
    public static bool IsValidSize(this IFormFile file, int size)
    {
        return file.Length <= size * 1024 * 1024;
    }

    public static bool IsValidType(this IFormFile file, string type)
    {
        return file.ContentType.StartsWith(type);
    }

    public static async Task<string> UploadAsync(this IFormFile file, params string[] paths)
    {
        string uploadPath = Path.Combine(paths);
        if (!Path.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        string newFile = Path.GetRandomFileName() + Path.GetExtension(file.FileName);

        using (Stream stream = File.Create(Path.Combine(uploadPath, newFile)))
        {
            await file.CopyToAsync(stream);
        }
        return newFile;
    }
}
