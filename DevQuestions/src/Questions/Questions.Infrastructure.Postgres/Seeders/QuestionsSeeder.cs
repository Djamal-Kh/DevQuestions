using Microsoft.EntityFrameworkCore;

namespace Questions.Infrastructure.Postgres.Seeders;

public class QuestionsSeeder : ISeeder
{
    private readonly DbContext _dbContext;

    public QuestionsSeeder(QuestionsDbContext dbContext, DbContext dbContext1)
    {
        _dbContext = dbContext1;
    }

    public Task SeedAsync()
    {
        throw new NotImplementedException();
    }
}