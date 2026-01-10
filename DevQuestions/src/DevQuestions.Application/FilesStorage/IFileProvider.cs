namespace DevQuestions.Application.FilesStorage;

public interface IFileProvider
{
    public Task<string> UploadAsync(Stream stream, string key, string bucket, CancellationToken cancellationToken);
    
    public Task<string> GetUrlByIdAsync(Guid fieldId, CancellationToken cancellationToken);
    
    public Task<Dictionary<Guid, string>> GetUrlsByIdAsync(IEnumerable<Guid> fieldIds, CancellationToken cancellationToken);
}