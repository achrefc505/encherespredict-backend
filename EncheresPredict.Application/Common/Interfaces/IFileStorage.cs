namespace EncheresPredict.Application.Common.Interfaces;

public interface IFileStorage
{
    Task<string> SaveAsync(
        byte[] content,
        string fileName,
        CancellationToken cancellationToken = default);
}