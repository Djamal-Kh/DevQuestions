namespace DevQuestions.Domain.Questions;

public class Answer
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public Question Question { get; set; }
}