using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Api.Tags.ViewModels;
using Notes.Core.Interfaces.IRepositories;
namespace Notes.Api.Tags.Queries;


public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, TagsVm>
{
    private readonly IRepository<Tag> _tagRepository;

    public GetTagsQueryHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    
    public async Task<TagsVm> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return new TagsVm {Tags = await _tagRepository.Items.ToListAsync()};
    }
}