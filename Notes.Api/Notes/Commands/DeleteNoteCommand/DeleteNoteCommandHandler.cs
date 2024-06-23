using MediatR;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
namespace Notes.Api.Notes.Commands;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, ValidatableResponse<int>>
{
    private readonly IRepository<Note> _repository;

    public DeleteNoteCommandHandler(IRepository<Note> repository)
    {
        _repository = repository;
    }
    
    public async Task<ValidatableResponse<int>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int id = request.Id!.Value;
            _repository.DeleteAsync(await _repository.GetAsync(id));
            return new ValidatableResponse<int> { Result = id };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<int>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError {Message = e.Message}}
            };
        }
    }
}