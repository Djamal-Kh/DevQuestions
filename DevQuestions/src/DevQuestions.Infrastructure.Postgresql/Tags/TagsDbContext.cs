using DevQuestions.Application.Tags;
using DevQuestions.Domain.Tags;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure.Postgresql;

public class TagsDbContext : DbContext, ITagsReadDbContext
{
    public DbSet<Tag> Tags { get; set; }

    public IQueryable<Tag> GetTags => Tags.AsNoTracking().AsQueryable();
}