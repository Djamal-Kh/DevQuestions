using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.FilesStorage;
using DevQuestions.Application.Tags;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace DevQuestions.Application.Questions.Features.GetQuestions;

public class GetQuestionsHandler : IQueryHandler<QuestionResponse, GetQuestionsQuery>
{
    private readonly IQuestionsDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly ITagsReadDbContext _tagsReadDbContext;

    public GetQuestionsHandler(IQuestionsDbContext context, IFileProvider fileProvider, ITagsReadDbContext tagsReadDbContext)
    {
        _context = context;
        _fileProvider = fileProvider;
        _tagsReadDbContext = tagsReadDbContext;
    }

    public async Task<QuestionResponse> Handle(GetQuestionsQuery command,
        CancellationToken cancellationToken)
    {
        var questions = await _context.GetQuestions
            .Include(q => q.Solution)
            .Skip(command.Dto.Page * command.Dto.PageSize - 1)
            .Take(command.Dto.PageSize)
            .ToListAsync(cancellationToken);

        long count = await _context.GetQuestions.LongCountAsync(cancellationToken);

        var screenshotIds = questions
            .Where(q => q.ScreenshotId is not null)
            .Select(q => q.ScreenshotId!.Value);

        var fileDict = await _fileProvider.GetUrlsByIdAsync(screenshotIds, cancellationToken);

        var questionTags = questions.SelectMany(q => q.Tags);

        var tags = await _tagsReadDbContext.GetTags
            .Where(t => questionTags.Contains(t.Id))
            .Select(t => t.Name)
            .ToListAsync(cancellationToken);

        var questionsDto = questions.Select(q => new QuestionDto(
            q.Id,
            q.Title,
            q.Text,
            q.UserId,
            q.ScreenshotId is not null ? fileDict[q.ScreenshotId.Value] : null,
            q.Solution?.Id,
            tags,
            q.Status.ToRussianString()
        ));

        return new QuestionResponse(questionsDto, count);
    }
}