using MediatR;

namespace Notes.Api.Notes.Commands;


public class CreateNoteCommand : IRequest<int>
{
    public string? Title { get; set; }
    public string? Text { get; set; }
}