using MediatR;
using Notes.Api.Notes.ViewModels;
namespace Notes.Api.Notes.Queries;

public class GetNoteQuery : IRequest<ValidatableResponse<NoteVm>>
{
    public int Id { get; set; }
}