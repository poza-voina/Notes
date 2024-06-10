using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Notes.Api.Notes.Commands;

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
    public IActionResult CreateNote([FromBody] CreateNoteCommand note)
    {
        _mediator.Send(note);
        return Ok();
    }
    
    
}