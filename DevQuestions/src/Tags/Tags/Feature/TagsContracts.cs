using Tags.Contracts;
using Tags.Contracts.Dtos;

namespace Tags.Feature;

public class TagsContracts : ITagsContract
{
    public Task<IReadOnlyList<TagDto>> GetByIdsAsync(GetByIdsDto dto, CancellationToken cancellationToken = default) => 
        throw new NotImplementedException();

    public Task CreateTagAsync(CreateTagDto dto) => throw new NotImplementedException();
}