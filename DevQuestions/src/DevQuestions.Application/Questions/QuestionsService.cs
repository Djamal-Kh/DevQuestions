using DevQuestions.Contracts;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger _logger;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(IQuestionsRepository questionsRepository, ILogger logger, IValidator<CreateQuestionDto> validator)
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Guid> CreateAsync(CreateQuestionDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        int countOfOpenUserQuestions = await _questionsRepository.GetOpenQuestionsCountAsync(request.UserId, cancellationToken);

        if (countOfOpenUserQuestions > 3)
        {
            throw new Exception("Too many open questions");
        }

        Guid questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            request.Title,
            request.Text,
            request.UserId,
            request.TagIds,
            null);

        var result = await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation($"Question created successfully with {questionId}", questionId);

        return questionId;
    }
}