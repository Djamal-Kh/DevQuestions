using DevQuestions.Application.Extensions;
using DevQuestions.Application.FullTextSearch;
using DevQuestions.Application.Questions.Exceptions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Contracts;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly ISearchProvider _searchProvider;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(IQuestionsRepository questionsRepository, ILogger<QuestionsService> logger, IValidator<CreateQuestionDto> validator, ISearchProvider searchProvider)
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _validator = validator;
        _searchProvider = searchProvider;
    }

    public async Task<Guid> CreateAsync(CreateQuestionDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            throw new QuestionValidationException(validationResult.ToErrors());
        }

        int countOfOpenUserQuestions = await _questionsRepository
            .GetOpenQuestionsCountAsync(request.UserId, cancellationToken);

        if (countOfOpenUserQuestions > 3)
        {
            throw new ToManyQuestionsException();
        }

        Guid questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            request.Title,
            request.Text,
            request.UserId,
            request.TagIds,
            null);

        await _searchProvider.IndexQuestionAsync(question);

        var result = await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation($"Question created successfully with {questionId}", questionId);

        return questionId;
    }
}