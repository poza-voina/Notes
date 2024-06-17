using Microsoft.AspNetCore.Mvc;
using MediatR;
using Notes.Api.Reminders.Commands;
using Notes.Api.Reminders.Queries;

namespace Notes.Api.Controllers;

[ApiController]
[Route("api/v1/function/reminder")]
public class RemindersController : ControllerBase
{
    private readonly IMediator _mediator;
    public RemindersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateNote([FromBody] CreateReminderCommand reminderCommand)
    {
        var result = await _mediator.Send(reminderCommand);
        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.ValidationFailures);
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> GetNoteById([FromQuery] GetReminderQuery getReminderQuery)
    {
        var result = await _mediator.Send(getReminderQuery);
        if (!result.IsValidationValid)
        {
            return BadRequest(result.ValidationFailures);
        }
        if (!result.IsProcessingValid)
        {
            return NotFound(result.ProcessingErrors);
        }
        return Ok(result.Result);
    }
}