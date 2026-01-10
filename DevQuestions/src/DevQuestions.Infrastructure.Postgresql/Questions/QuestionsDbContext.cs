using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure.Postgresql;

public class QuestionsDbContext : DbContext, IQuestionsDbContext
{
    public DbSet<Question> Questions { get; set; }

    public IQueryable<Question> GetQuestions => Questions.AsNoTracking().AsQueryable();
}