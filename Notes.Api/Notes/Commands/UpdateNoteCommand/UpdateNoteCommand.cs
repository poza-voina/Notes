using MediatR;
using Notes.Api.Notes.ViewModels;
namespace Notes.Api.Notes.Commands;

public class UpdateNoteCommand : IRequest<ValidatableResponse<NoteVm>>
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    
    public ICollection<string>? TagsTitles { get; set; }
}