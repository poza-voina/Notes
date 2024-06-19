using MediatR;

namespace Notes.Api.Tags.Commands;

public class UnbindTagFromNoteCommand : IRequest<ValidatableResponse<int>>
{
    public int? TagId { get; set; }
    public int? NoteId { get; set; }
}