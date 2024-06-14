using Notes.Core.Entities;
using MediatR;
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
        if (!result.IsValid)
        {
            return BadRequest(result.ValidationFailures);
        }
        return Ok();
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
}