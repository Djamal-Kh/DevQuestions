using Microsoft.EntityFrameworkCore;
using Questions.Application;
using Questions.Domain;

namespace Questions.Infrastructure.Postgres;

public class QuestionsDbContext : DbContext, IQuestionsDbContext
{
    public DbSet<Question> Questions { get; set; }

    public IQueryable<Question> GetQuestions => Questions.AsNoTracking().AsQueryable();
}