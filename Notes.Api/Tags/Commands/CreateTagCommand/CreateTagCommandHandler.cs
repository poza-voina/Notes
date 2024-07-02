using MediatR;
using Notes.Core.Interfaces.IRepositories;
using Notes.Core.Entities;
using Notes.Api.Tags.ViewModels;
using Notes.Infrastructure.Repositories;

namespace Notes.Api.Tags.Commands;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ValidatableResponse<TagVm>>
{
    private readonly ITagRepository _tagRepository;

    public CreateTagCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ValidatableResponse<TagVm>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        Tag tag;
        try
        {
            tag = await _tagRepository.GetTagByTitleAsync(request.Title!);
        }
        catch (ArgumentException e)
        {
            tag = await _tagRepository.CreateAsync(new Tag { Title = request.Title! });
            return new ValidatableResponse<TagVm> { Result = new TagVm {Title = tag.Title, Id = tag.Id} };
        }
        
        return new ValidatableResponse<TagVm>
        {
            ProcessingErrors = new List<ProcessingError>
            {
                new ProcessingError($"tag with {request.Title!} already exists", ProcessingErrors.Conflict)
            }
        };

    }
}


