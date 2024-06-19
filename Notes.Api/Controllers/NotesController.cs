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
            return Ok(result.Result);
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

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllNotes([FromQuery] GetNotesQuery getNotesQuery)
    {
        var result = await _mediator.Send(getNotesQuery);
        return Ok(result);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteNoteById([FromBody] DeleteNoteCommand deleteNoteCommand)
    {
        var result = await _mediator.Send(deleteNoteCommand);
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
    public async Task<IActionResult> UpdateNoteById([FromBody] UpdateNoteCommand updateNoteCommand)
    {
        var result = await _mediator.Send(updateNoteCommand);
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