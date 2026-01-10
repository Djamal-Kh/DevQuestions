using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Questions.Contracts;
using Questions.Domain;
using Shared;
using Shared.Abstractions;
using Shared.Database;
using Shared.Extensions;

namespace Questions.Application.Features.AddAnswer;

public class AddAnswerHandler : ICommandHandler<Guid, AddAnswerCommand>
{
    private readonly IValidator<AddAnswerDto> _validator;
    private readonly ITransactionManager _transactionManager;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<AddAnswerHandler> _logger;
    
    public AddAnswerHandler(ITransactionManager transactionManager, IQuestionsRepository questionsRepository, ILogger<AddAnswerHandler> logger)
    {
        _transactionManager = transactionManager;
        _questionsRepository = questionsRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid, Errors>> Handle(AddAnswerCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.AddAnswerDto);

        if(!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        var questionResult = await _questionsRepository.GetByIdAsync(command.QuestiondId, cancellationToken);
        if (questionResult.IsFailure)
            return questionResult.Error;

        var question = questionResult.Value;

        var answer = new Answer(Guid.NewGuid(), command.AddAnswerDto.UserId, command.AddAnswerDto.Text, command.QuestiondId);

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
