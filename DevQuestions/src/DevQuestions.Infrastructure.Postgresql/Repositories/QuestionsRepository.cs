using Dapper;
using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DevQuestions.Infrastructure.Postgresql.Repositories;

public class QuestionsRepository : IQuestionsRepository
{
    private readonly QuestionsDbContext _dbContext;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public QuestionsRepository(QuestionsDbContext dbContext, IConfiguration configuration, ISqlConnectionFactory sqlConnectionFactory)
    {
        _dbContext = dbContext;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO questions (id, title, text, user_id, tag_ids, screenshot_id)
            VALUES (@id, @title, @text, @userId, @tagIds, @screenshotId)
        """;
        
        using var connection = _sqlConnectionFactory.CreateConnection();

        await connection.ExecuteAsync(sql,
            new
            {
                Id = question.Id,
                Title = question.Title,
                Text = question.Text,
                UserId = question.UserId,
                TagIds = question.Tags.ToArray(),
                ScreenshotId = question.ScreenshotId
            });

        return question.Id;
    }

    public Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<Question?> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        var question = await _dbContext.Questions
            .Include(q => q.Answers)
            .Include(q => q.Solution)
            .FirstOrDefaultAsync(q => q.Id == questionId, cancellationToken);

        return question;
    }

    public Task<List<Question>> GetAllAsync(CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<int> GetOpenQuestionsCountAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
}