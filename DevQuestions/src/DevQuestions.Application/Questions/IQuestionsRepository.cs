using CSharpFunctionalExtensions;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionsRepository
{
    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken);
    Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken);

    Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken);

    Task<Result<Question, Errors>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken);

    Task<int> GetOpenQuestionsCountAsync(Guid userId, CancellationToken cancellationToken);
}