using Microsoft.EntityFrameworkCore;
using Tags.Domain;

namespace Tags.Database;

public class TagsDbContext : DbContext, ITagsDbContext
{
    public DbSet<Tag> Tags { get; set; }

    public IQueryable<Tag> GetTags => Tags.AsNoTracking().AsQueryable();
}