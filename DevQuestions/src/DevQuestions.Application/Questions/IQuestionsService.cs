using DevQuestions.Contracts;

namespace DevQuestions.Application.Questions;

public interface IQuestionsService
{
    Task<Guid> CreateAsync(CreateQuestionDto request, CancellationToken cancellationToken);
}