namespace DevQuestions.Domain.Reports;

public class Report
{
    public Guid Id { get; set; }

    public string ReportText { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public Guid ReportedUserId { get; set; }

    public string Reason { get; set; } = string.Empty;

    public ReportStatus ReportStatus { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid ResolvedByUserId { get; set; }
}