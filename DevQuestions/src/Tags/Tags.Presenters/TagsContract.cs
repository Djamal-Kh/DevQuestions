using Shared.Abstractions;
using Tags.Contracts;
using Tags.Contracts.Dtos;
using Tags.Database;
using Tags.Feature;

namespace Tags.Presenters;

public class TagsContract : ITagsContract
{
    private readonly TagsDbContext _context;
    private readonly IQueryHandler<IReadOnlyList<TagDto>, GetByIdsQuery> _handler;

    public TagsContract(TagsDbContext context, 
        IQueryHandler<IReadOnlyList<TagDto>, GetByIdsQuery> handler)
    {
        _context = context;
        _handler = handler;
    }

    public async Task<IReadOnlyList<TagDto>> GetByIdsAsync(GetByIdsDto dto, CancellationToken cancellationToken)
    {
        return await _handler.Handle(new GetByIdsQuery(dto), cancellationToken);
    }

    public async Task CreateTagAsync(CreateTagDto dto)
    {
        await Create.Handler(dto, _context);
    }
}