using MediatR;
using Notes.Infrastructure.Repositories;
using Notes.Core.Entities;
using Notes.Infrastructure.Services;

namespace Notes.Api.Tags.Commands;

public class BindTagToReminderCommandHandler : IRequestHandler<BindTagToReminderCommand, ValidatableResponse<int>>
{
    private readonly ITagsService _tagsService;

    public BindTagToReminderCommandHandler(ITagsService tagsService)
    {
        _tagsService = tagsService;
    }
    public async Task<ValidatableResponse<int>> Handle(BindTagToReminderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _tagsService.AddTagToReminderAsync(request.TagId!.Value, request.ReminderId!.Value);
            return new ValidatableResponse<int> { Result = request.TagId.Value };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<int>
                { ProcessingErrors = new List<ProcessingError> { new ProcessingError { Message = e.Message } } };
        }
    }
}


