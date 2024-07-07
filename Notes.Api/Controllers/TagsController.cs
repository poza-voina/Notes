using System.Diagnostics.CodeAnalysis;
using Notes.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Tags.Commands;
using Notes.Api.Tags.Queries;

namespace Notes.Api.Controllers;

[ApiController]
[Route("api/v1/function/tag")]
public class TagsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand createTagCommand)
    {
        var result = await _mediator.Send(createTagCommand);
        if (!result.IsValidationValid)
        {
            return BadRequest(result.ValidationFailures);
        }
        
        if (!result.IsProcessingValid)
        {
            if (result.ProcessingErrors!.First().Type == ProcessingErrors.NotFound)
            {
                return NotFound(result.ProcessingErrors);
            }

            if (result.ProcessingErrors!.First().Type == ProcessingErrors.Conflict)
            {
                return Conflict(result.ProcessingErrors);
            }
        }

        return Ok(result.Result);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetTagById([FromQuery] GetTagQuery getTagQuery)
    {
        var result = await _mediator.Send(getTagQuery);
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

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllTags([FromQuery] GetTagsQuery getTagsQuery) =>
        Ok(await _mediator.Send(getTagsQuery));


    [HttpPost("delete")]
    public async Task<IActionResult> DeleteTagById([FromBody] DeleteTagCommand deleteTagCommand)
    {
        var result = await _mediator.Send(deleteTagCommand);
        if (!result.IsValidationValid)
        {
            return BadRequest(result.ValidationFailures);
        }

        if (!result.IsProcessingValid)
        {
            return NotFound(result.ProcessingErrors);
        }

        return Ok();
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateTagById([FromBody] UpdateTagCommand updateTagCommand)
    {
        var result = await _mediator.Send(updateTagCommand);
        if (!result.IsValidationValid)
        {
            return BadRequest(result.ValidationFailures);
        }

        if (!result.IsProcessingValid)
        {
            if (result.ProcessingErrors!.First().Type == ProcessingErrors.Conflict)
            {
                return Conflict(result.ProcessingErrors);
            }

            if (result.ProcessingErrors!.First().Type == ProcessingErrors.NotFound)
            {
                return NotFound(result.ProcessingErrors);

            }
            
        }

        return Ok(result.Result);
    }

    [HttpPost("bind-to-note")]
    public async Task<IActionResult> BindToNote([FromBody] BindTagToNoteCommand bindTagToNoteCommand)
    {
        var result = await _mediator.Send(bindTagToNoteCommand);
        if (!result.IsValidationValid)
        {
            return BadRequest(result.ValidationFailures);
        }

        if (!result.IsProcessingValid)
        {
            return NotFound(result.ProcessingErrors);
        }

        return Ok();
    }
    
    [HttpPost("bind-to-reminder")]
    public async Task<IActionResult> BindToReminder([FromBody] BindTagToReminderCommand bindTagToReminder)
    {
        var result = await _mediator.Send(bindTagToReminder);
        if (!result.IsValidationValid)
        {
            return BadRequest(result.ValidationFailures);
        }

        if (!result.IsProcessingValid)
        {
            return NotFound(result.ProcessingErrors);
        }

        return Ok();
    }

    [HttpPost("unbind-from-note")]
    public async Task<IActionResult> UnbindFromNote([FromBody] UnbindTagFromNoteCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsValidationValid)
        {
            return BadRequest(result.ValidationFailures);
        }

        if (!result.IsProcessingValid)
        {
            return NotFound(result.ProcessingErrors);
        }

        return Ok();
    }
    
    [HttpPost("unbind-from-reminder")]
    public async Task<IActionResult> UnbindFromReminder([FromBody] UnbindTagFromReminderCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsValidationValid)
        {
            return BadRequest(result.ValidationFailures);
        }

        if (!result.IsProcessingValid)
        {
            return NotFound(result.ProcessingErrors);
        }

        return Ok();
    }
}