using MediatR;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
namespace Notes.Api.Tags.Commands;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, ValidatableResponse<int>>
{
    private readonly IRepository<Tag> _tagRepository;

    public DeleteTagCommandHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ValidatableResponse<int>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int id = request.Id!.Value;
            _tagRepository.DeleteAsync(await _tagRepository.GetAsync(id));
            return new ValidatableResponse<int>{Result = id};
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