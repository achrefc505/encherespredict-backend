using EncheresPredict.Application.Common.Interfaces;

namespace EncheresPredict.Infrastructure.Storage;

public sealed class LocalFileStorage : IFileStorage
{
    private readonly string _rootFolder;

    public LocalFileStorage()
    {
        _rootFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Storage");

        Directory.CreateDirectory(_rootFolder);
    }

    public async Task<string> SaveAsync(
        byte[] content,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var uniqueName =
            $"{Guid.NewGuid()}_{fileName}";

        var fullPath =
            Path.Combine(_rootFolder, uniqueName);

        await File.WriteAllBytesAsync(
            fullPath,
            content,
            cancellationToken);

        return fullPath;
    }
}