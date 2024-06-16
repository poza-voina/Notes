using MediatR;
using Notes.Api;
namespace Notes.Api.Notes.Commands;


public class CreateNoteCommand : IRequest<ValidatableResponse<int>>
{
    public string? Title { get; set; }
    public string? Text { get; set; }
    
    public ICollection<string>? Tags { get; set; } 
}