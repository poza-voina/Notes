using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Api.Tags.ViewModels;
using Npgsql;

namespace Notes.Api.Tags.Commands;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, ValidatableResponse<TagVm>>
{
    private readonly ITagRepository _tagRepository;

    public UpdateTagCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ValidatableResponse<TagVm>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        Tag tag;
        try
        {
            tag = await _tagRepository.GetAsync(request.Id!.Value);
            tag.Title = request.Title!;
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<TagVm>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError { Message = e.Message } }
            };
        }
        
        if ((await _tagRepository.GetTagByTitleOrDefaultAsync(request.Title!)) is not null)
        {
            return new ValidatableResponse<TagVm>
            {
                ProcessingErrors = new List<ProcessingError>
                    { new ProcessingError { Message = $"tag with {tag.Title} already exists" , Type = ProcessingErrors.Conflict}}
            };
        }
        

        await _tagRepository.UpdateAsync(tag);
        return new ValidatableResponse<TagVm> { Result = new TagVm {Title = tag.Title, Id = tag.Id} };
    }
}