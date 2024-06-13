using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Notes.Api.Notes.Commands;
using Notes.Api.Notes.Queries;

namespace Notes.Api.Controllers;

[ApiController]
[Route("api/v1/function/note")]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotesController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateNote([FromBody] CreateNoteCommand note)
    {
        var result = await _mediator.Send(note);
        if (result.IsValid)
        {
            return Ok();
        }

        return BadRequest(result.ValidationFailures);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetNoteById([FromQuery] GetNoteQuery getNoteQuery)
    {
        var result = await _mediator.Send(getNoteQuery);
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