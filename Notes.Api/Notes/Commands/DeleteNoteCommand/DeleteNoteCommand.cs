using MediatR;

namespace Notes.Api.Notes.Commands;

public class DeleteNoteCommand : IRequest<ValidatableResponse<int>>
{
    public int? Id { get; set; }
}