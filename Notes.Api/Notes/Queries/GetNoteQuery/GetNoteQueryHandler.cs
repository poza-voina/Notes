using MediatR;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Api.Notes.ViewModels;

namespace Notes.Api.Notes.Queries;

public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, ValidatableResponse<NoteVm>>
{
    private readonly IRepository<Note> _noteRepository;

    public GetNoteQueryHandler(IRepository<Note> noteRepository)
    {
        _noteRepository = noteRepository;
    }
    public async Task<ValidatableResponse<NoteVm>> Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var note = await _noteRepository.GetAsync(request.Id);
            return new ValidatableResponse<NoteVm>
                { Result = new NoteVm { Id = note.Id, Title = note.Title, Text = note.Text, Tags = note.Tags} };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<NoteVm>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError {Message = e.Message} }
            };
        }
        
    }
}