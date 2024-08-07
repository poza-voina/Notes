using MediatR;
using Notes.Core.Entities;

namespace Notes.Api.Tags.Commands;


public class BindTagToNoteCommand : IRequest<ValidatableResponse<int>>
{
    public int? TagId { get; set; }
    public int? NoteId { get; set; }
} 