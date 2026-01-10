using Questions.Contracts;
using Shared.Abstractions;

namespace Questions.Application.Features.CreateQuestion;

public record CreateQuestionCommand(CreateQuestionDto QuestionDto) : ICommand;