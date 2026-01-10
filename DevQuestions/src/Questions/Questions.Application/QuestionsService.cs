using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Questions.Application.Fails;
using Questions.Contracts;
using Questions.Domain;
using Shared;
using Shared.Database;
using Shared.Extensions;

namespace Questions.Application;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _createQuestionDtovalidator;
    private readonly IValidator<AddAnswerDto> _addAnswerDtoValidator;
    private readonly ITransactionManager _transactionManager;

    public QuestionsService(
        IQuestionsRepository questionsRepository, 
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> createQuestionDtovalidator, 
        IValidator<AddAnswerDto> addAnswerDtoValidator,
        ITransactionManager transactionManager)
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _createQuestionDtovalidator = createQuestionDtovalidator;
        _addAnswerDtoValidator = addAnswerDtoValidator;
        _transactionManager = transactionManager;
    }

    public async Task<Result<Guid, Errors>> CreateAsync(CreateQuestionDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _createQuestionDtovalidator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        int countOfOpenUserQuestions = await _questionsRepository
            .GetOpenQuestionsCountAsync(request.UserId, cancellationToken);

        var calculator = new QuestionCalculator();

        var calclulateResult = calculator.Calculate();
        if (calclulateResult.IsFailure)
        {
            return calclulateResult.Error.ToErrors();
        }

        if (countOfOpenUserQuestions > 3)
        {
            return Errors1.Questions.ToManyQuestions().ToErrors();
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

    public async Task<Result<Guid, Errors>> AddAnswer(Guid questionId, AddAnswerDto addAnswerDto, CancellationToken cancellationToken)
    {
        var validationResult = await _addAnswerDtoValidator.ValidateAsync(addAnswerDto);

        if(!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        var questionResult = await _questionsRepository.GetByIdAsync(questionId, cancellationToken);
        if (questionResult.IsFailure)
            return questionResult.Error;

        var question = questionResult.Value;

        var answer = new Answer(Guid.NewGuid(), addAnswerDto.UserId, addAnswerDto.Text, questionId);

        question.Answers.Add(answer);

        var answerId = await _questionsRepository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation($"Answer created successfully with {answerId}", answer.Id);

        return answer.Id;
    }
}

public class QuestionCalculator
{
    public Result<int, Error> Calculate()
    {
        return Error.Failure("", "");
    }
}
