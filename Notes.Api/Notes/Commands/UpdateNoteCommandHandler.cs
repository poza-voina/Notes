using System.Diagnostics;
using MediatR;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
namespace Notes.Api.Notes.Commands;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, ValidatableResponse<int>>
{
    private readonly IRepository<Note> _repository;

    public UpdateNoteCommandHandler(IRepository<Note> repository)
    {
        _repository = repository;
    }


    public async Task<ValidatableResponse<int>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var note = await _repository.GetAsync(request.Id!.Value);
            note.Text = request.Text ?? note.Text;
            note.Title = request.Title ?? note.Title;
            await _repository.UpdateAsync(note);
            return new ValidatableResponse<int> { Result = note.Id };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<int>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError { Message = e.Message } }
            };
        }
    }
}