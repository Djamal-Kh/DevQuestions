using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure.Postgresql.Seeders;

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