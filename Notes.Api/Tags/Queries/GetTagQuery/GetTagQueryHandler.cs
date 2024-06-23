using MediatR;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Api.Tags.Queries.ViewModels;

namespace Notes.Api.Tags.Queries;

public class GetTagQueryHandler : IRequestHandler<GetTagQuery, ValidatableResponse<TagVm>>
{
    private readonly IRepository<Tag> _tagRepository;

    public GetTagQueryHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<ValidatableResponse<TagVm>> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var tag = await _tagRepository.GetAsync(request.Id!.Value);
            return new ValidatableResponse<TagVm>
            {
                Result = new TagVm { Title = tag.Title }
            };
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