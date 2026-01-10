namespace Questions.Contracts;

public record QuestionResponse(IEnumerable<QuestionDto> Questions, long TotalCount);