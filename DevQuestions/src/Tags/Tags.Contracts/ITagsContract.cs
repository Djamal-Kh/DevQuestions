using Tags.Contracts.Dtos;

namespace Tags.Contracts;

public interface ITagsContract
{
    Task<IReadOnlyList<TagDto>> GetByIdsAsync(GetByIdsDto dto, CancellationToken cancellationToken = default);
    Task CreateTagAsync(CreateTagDto dto);
}