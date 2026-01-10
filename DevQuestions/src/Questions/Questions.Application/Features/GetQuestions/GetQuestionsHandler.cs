using Microsoft.EntityFrameworkCore;
using Questions.Contracts;
using Questions.Domain;
using Shared.Abstractions;
using Shared.FilesStorage;
using Tags.Contracts;
using Tags.Contracts.Dtos;

namespace Questions.Application.Features.GetQuestions;

public class GetQuestionsHandler : IQueryHandler<QuestionResponse, GetQuestionsQuery>
{
    private readonly IQuestionsDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly ITagsContract _tagsContract;

    public GetQuestionsHandler(
        IQuestionsDbContext context,
        ITagsContract tagsContract,
        IFileProvider fileProvider)
    {
        _context = context;
        _tagsContract = tagsContract;
        _fileProvider = fileProvider;
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

        var tags = await _tagsContract.GetByIdsAsync(new GetByIdsDto(questionTags.ToArray()));

        var questionsDto = questions.Select(q => new QuestionDto(
            q.Id,
            q.Title,
            q.Text,
            q.UserId,
            q.ScreenshotId is not null ? fileDict[q.ScreenshotId.Value] : null,
            q.Solution?.Id,
            tags.Select(t => t.Name),
            q.Status.ToRussianString()
        ));

        return new QuestionResponse(questionsDto, count);
    }
}