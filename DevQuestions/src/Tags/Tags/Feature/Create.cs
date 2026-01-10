using Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Tags.Contracts.Dtos;
using Tags.Database;
using Tags.Domain;

namespace Tags.Feature;

public sealed class Create
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/tags", Handler);
        }
    }

    public sealed class CreateTagHandler
    {
        private readonly TagsDbContext _context;

        public CreateTagHandler(TagsDbContext context)
        {
            _context = context;
        }

        private async Task<IResult> Handle(
            CreateTagDto dto,
            TagsDbContext context)
        {
            var tag = new Tag() { Name = dto.Name, };

            await context.AddAsync(tag);

            return Results.Ok(tag.Id);
        }
    }

    public static async Task<IResult> Handler(
        CreateTagDto dto,
        TagsDbContext context)
    {
        var tag = new Tag
        {
            Name = dto.Name,
        };

        await context.AddAsync(tag);

        return Results.Ok(tag.Id);
    }
}