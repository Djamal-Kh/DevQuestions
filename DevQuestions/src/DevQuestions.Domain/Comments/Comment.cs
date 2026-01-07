namespace DevQuestions.Domain.Comments;

public class Comment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Text { get; set; } = string.Empty;

    public Comment? Parent { get; set; }

    public List<Comment> Children { get; set; }

    public Guid EntityId { get; set; }

    public string EntityType { get; set; } = string.Empty;
}