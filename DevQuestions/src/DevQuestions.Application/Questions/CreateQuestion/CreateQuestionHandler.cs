using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.CreateQuestion;

public class CreateQuestionHandler : ICommandHandler<Guid, CreateQuestionCommand>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _validator;
    private readonly ILogger<CreateQuestionHandler> _logger;

    public CreateQuestionHandler(IQuestionsRepository questionsRepository, IValidator<CreateQuestionDto> validator, ILogger<CreateQuestionHandler> logger)
    {
        _questionsRepository = questionsRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, Errors>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.QuestionDto, cancellationToken);

        if(!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        int countOfOpenUserQuestions = await _questionsRepository
            .GetOpenQuestionsCountAsync(command.QuestionDto.UserId, cancellationToken);

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
            command.QuestionDto.Title,
            command.QuestionDto.Text,
            command.QuestionDto.UserId,
            command.QuestionDto.TagIds,
            null);

        var result = await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation($"Question created successfully with {questionId}", questionId);

        return questionId;
    }
}