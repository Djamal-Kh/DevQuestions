using Framework.ResponseExtensions;
using Microsoft.AspNetCore.Mvc;
using Questions.Application;
using Questions.Application.Features.AddAnswer;
using Questions.Application.Features.CreateQuestion;
using Questions.Application.Features.GetQuestions;
using Questions.Contracts;
using Shared.Abstractions;

namespace Questions.Presenters;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionsService _questionsService;

    public QuestionsController(IQuestionsService questionsService)
    {
        _questionsService = questionsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] ICommandHandler<Guid, CreateQuestionCommand> handler,
        [FromBody] CreateQuestionDto questionDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateQuestionCommand(questionDto);

        var result = await handler.Handle(command, cancellationToken);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromServices] IQueryHandler<QuestionResponse, GetQuestionsQuery> handler,
        [FromQuery] GetQuestionsDto questionsDto,
        CancellationToken cancellationToken)
    {
        var command = new GetQuestionsQuery(questionsDto);

        var result = await handler.Handle(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{questionId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid questionId, CancellationToken cancellationToken)
    {
        return Ok("Get Question by Id");
    }

    [HttpPut("{questionId}")]
    public async Task<IActionResult> Update([FromRoute] Guid questionId, [FromBody] UpdateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        return Ok("Question updated");
    }

    [HttpDelete("{questionId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid questionId, CancellationToken cancellationToken)
    {
        return Ok("Question deleted");
    }
    
    [HttpPut("{questionId}/solution")]
    public async Task<IActionResult> SelectSolution([FromRoute] Guid questionId,[FromQuery]Guid answerId, CancellationToken cancellationToken)
    {
        return Ok("Solution is selected");
    }

    [HttpPost("{questionId:guid}/answers")]
    public async Task<IActionResult> AddAnswer(
        [FromServices] ICommandHandler<Guid, AddAnswerCommand> handler,
        [FromRoute] Guid questionId,
        [FromBody] AddAnswerDto answerDto,
        CancellationToken cancellationToken)
    {
        var command = new AddAnswerCommand(questionId, answerDto);

        var result = await handler.Handle(command, cancellationToken);
        return result.IsFailure ? result.Error.ToResponse() : Ok(result.Value);
        return Ok("Answer added");
    }
}