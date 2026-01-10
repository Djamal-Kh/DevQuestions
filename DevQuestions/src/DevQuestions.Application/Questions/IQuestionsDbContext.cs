using DevQuestions.Domain.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionsDbContext
{
    IQueryable<Question> GetQuestions { get; }
}