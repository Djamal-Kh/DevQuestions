using Questions.Contracts;
using Shared.Abstractions;

namespace Questions.Application.Features.AddAnswer;

public record AddAnswerCommand(Guid QuestiondId, AddAnswerDto AddAnswerDto) : ICommand;