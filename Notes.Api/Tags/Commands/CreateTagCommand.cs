using MediatR;
namespace Notes.Api.Tags.Commands;


public class CreateTagCommand : IRequest<ValidatableResponse<int>>
{
    public string? Title { get; set; }
} 