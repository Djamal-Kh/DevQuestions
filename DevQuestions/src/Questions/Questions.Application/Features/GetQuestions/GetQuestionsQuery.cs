using Questions.Contracts;
using Shared.Abstractions;

namespace Questions.Application.Features.GetQuestions;

public record GetQuestionsQuery(GetQuestionsDto Dto) : IQuery;