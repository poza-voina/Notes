using MediatR;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Api.Tags.ViewModels;

namespace Notes.Api.Tags.Commands;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, ValidatableResponse<TagVm>>
{
    private readonly IRepository<Tag> _tagRepository;

    public UpdateTagCommandHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ValidatableResponse<TagVm>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var tag = await _tagRepository.GetAsync(request.Id!.Value);
            tag.Title = request.Title!;
            await _tagRepository.UpdateAsync(tag);
            return new ValidatableResponse<TagVm> { Result = new TagVm {Title = tag.Title, Id = tag.Id} };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<TagVm>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError { Message = e.Message } }
            };
        }
        
    }
}