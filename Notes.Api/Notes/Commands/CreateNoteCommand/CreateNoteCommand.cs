using MediatR;
using Notes.Api;
using Notes.Api.Notes.ViewModels;
namespace Notes.Api.Notes.Commands;


public class CreateNoteCommand : IRequest<ValidatableResponse<NoteVm>>
{
    public string? Title { get; set; }
    public string? Text { get; set; }
    
    public ICollection<string>? TagsTitles { get; set; } 
}