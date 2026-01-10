namespace DevQuestions.Contracts.Questions;

public record QuestionResponse(IEnumerable<QuestionDto> Questions, long TotalCount);