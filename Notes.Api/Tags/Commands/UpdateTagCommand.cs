using MediatR;

namespace Notes.Api.Tags.Commands;

public class UpdateTagCommand : IRequest<ValidatableResponse<int>>
{
    public int? Id { get; set; }
    public string? Title { get; set; }
}