using CSharpFunctionalExtensions;
using Questions.Contracts;
using Shared;

namespace Questions.Application;

public interface IQuestionsService
{
    /// <summary>
    /// Создание вопроса
    /// </summary>
    /// <param name="questionDto">QuestionDto</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id созданного вопроса или список ошибок</returns>
    Task<Result<Guid, Errors>> CreateAsync(CreateQuestionDto questionDto, CancellationToken cancellationToken);

    /// <summary>
    /// Добавление ответа на вопрос
    /// </summary>
    /// <param name="questionId">Id вопроса</param>
    /// <param name="addAnswerDto">DTO для добавления ответа на вопрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id созданного ответа или список ошибок</returns>
    Task<Result<Guid, Errors>> AddAnswer(Guid questionId, AddAnswerDto addAnswerDto, CancellationToken cancellationToken);
}