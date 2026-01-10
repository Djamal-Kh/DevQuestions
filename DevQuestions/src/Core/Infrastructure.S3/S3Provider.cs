using Shared.FilesStorage;

namespace Infrastructure.S3;

public class S3Provider : IFileProvider
{
    public Task<string> UploadAsync(Stream stream, string key, string bucket, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<string> GetUrlByIdAsync(Guid fieldId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Dictionary<Guid, string>> GetUrlsByIdAsync(IEnumerable<Guid> fieldIds, CancellationToken cancellationToken) => throw new NotImplementedException();
}