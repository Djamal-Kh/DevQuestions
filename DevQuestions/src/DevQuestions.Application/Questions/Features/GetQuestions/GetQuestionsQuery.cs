using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions.Features.GetQuestions;

public record GetQuestionsQuery(GetQuestionsDto Dto) : IQuery;