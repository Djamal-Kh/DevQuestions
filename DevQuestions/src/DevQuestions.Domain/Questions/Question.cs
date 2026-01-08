namespace DevQuestions.Domain.Questions;

public class Question
{
    public Question(
        Guid id,
        string title,
        string text,
        Guid userId,
        IEnumerable<Guid> tags,
        Guid? screenshotId)
    {
        Id = id;
        Title = title;
        Text = text;
        UserId = userId;
        Tags = tags.ToList();
        ScreenshotId = screenshotId;
    }
    
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public List<Answer> Answers { get; set; } = [];

    public Answer? Solution { get; set; }

    public List<Guid> Tags { get; set; }

    public Guid? ScreenshotId { get; set; }

    public QuestionStatus Status { get; set; } = QuestionStatus.OPEN;
}

public enum QuestionStatus
{
    /// <summary>
    /// Статус открыт
    /// </summary>
    OPEN,

    /// <summary>
    /// Статус решен
    /// </summary>
    RESOLVED,
}