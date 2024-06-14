using Notes.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Tags.Commands;

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
        if (!result.IsValid)
        {
            return BadRequest(result.ValidationFailures);
        }
        return Ok();
    }
}