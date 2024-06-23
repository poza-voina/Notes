using MediatR;
using Notes.Core.Interfaces.IServices;

namespace Notes.Api.Tags.Commands;

public class UnbindTagFromReminderCommandHandler : IRequestHandler<UnbindTagFromReminderCommand, ValidatableResponse<int>>
{
    private readonly ITagsService _tagsService;
    public UnbindTagFromReminderCommandHandler(ITagsService tagsService)
    {
        _tagsService = tagsService;
    }

    public async Task<ValidatableResponse<int>> Handle(UnbindTagFromReminderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _tagsService.UnbindTagFromReminderAsync(request.TagId!.Value, request.ReminderId!.Value);
            return new ValidatableResponse<int> { Result = request.TagId.Value };
        }
        catch (InvalidOperationException e)
        {
            return new ValidatableResponse<int>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError { Message = e.Message } }
            };
        }
    }
}