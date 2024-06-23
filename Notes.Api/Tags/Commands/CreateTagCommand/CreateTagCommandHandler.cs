using MediatR;
using Notes.Core.Interfaces.IRepositories;
using Notes.Core.Entities;
using Notes.Api.Tags.ViewModels;

namespace Notes.Api.Tags.Commands;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ValidatableResponse<TagVm>>
{
    private readonly IRepository<Tag> _tagRepository;

    public CreateTagCommandHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ValidatableResponse<TagVm>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.CreateAsync(new Tag { Title = request.Title! });
        return new ValidatableResponse<TagVm> { Result = new TagVm {Title = tag.Title, Id = tag.Id} };
    }
}


