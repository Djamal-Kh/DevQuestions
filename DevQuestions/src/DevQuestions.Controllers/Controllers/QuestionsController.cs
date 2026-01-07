using Microsoft.AspNetCore.Mvc;
using DevQuestions.Contracts;

namespace DevQuestions.Controllers.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        return Ok("Question Created");
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetQuestionsDto questionsDto, CancellationToken cancellationToken)
    {
        return Ok("Get Questions");
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
    public async Task<IActionResult> AddAnswer([FromRoute] Guid questionId, [FromBody] AddAnswerDto answerDto, CancellationToken cancellationToken)
    {
        return Ok("Answer added");
    }
}