using Questions.Domain;

namespace Questions.Application;

public interface IQuestionsDbContext
{
    IQueryable<Question> GetQuestions { get; }
}