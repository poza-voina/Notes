using MediatR;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;

namespace Notes.Api.Tags.Commands;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, ValidatableResponse<int>>
{
    private readonly IRepository<Tag> _tagRepository;

    public UpdateTagCommandHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ValidatableResponse<int>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var tag = await _tagRepository.GetAsync(request.Id!.Value);
            tag.Title = request.Title!;
            await _tagRepository.UpdateAsync(tag);
            return new ValidatableResponse<int> { Result = tag.Id };
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