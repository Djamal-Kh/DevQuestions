using Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Shared.Abstractions;
using Tags.Contracts.Dtos;
using Tags.Database;

namespace Tags.Feature;

public record GetByIdsQuery(GetByIdsDto Dto) : IQuery;

public class GetByIds
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            // используем POST потому что необходимо передать параметры через тело,
            // а не через URL чтобы избежать очень длинных URL
            app.MapPost("tags/ids", async (
                GetByIdsDto dto,
                IQueryHandler<IReadOnlyList<TagDto>, GetByIdsQuery> handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(new GetByIdsQuery(dto), cancellationToken);

                return Results.Ok(result);
            });
        }
    }

    public sealed class Handler : IQueryHandler<IReadOnlyList<TagDto>, GetByIdsQuery>
    {
        private readonly TagsDbContext _context;

        public Handler(TagsDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TagDto>> Handle(
            GetByIdsQuery query,
            CancellationToken cancellationToken)
        {
            var tags = await _context.Tags
                .Where(x => query.Dto.Ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            return tags.Select(t => new TagDto(t.Id, t.Name, t.Description)).ToList();
        }
    }
}