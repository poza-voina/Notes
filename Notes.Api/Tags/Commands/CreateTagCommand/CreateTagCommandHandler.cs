using MediatR;
using Notes.Core.Interfaces.IRepositories;
using Notes.Core.Entities;

namespace Notes.Api.Tags.Commands;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ValidatableResponse<int>>
{
    private readonly IRepository<Tag> _tagRepository;

    public CreateTagCommandHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ValidatableResponse<int>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.CreateAsync(new Tag { Title = request.Title! });
        return new ValidatableResponse<int> { Result = tag.Id };
    }
}


