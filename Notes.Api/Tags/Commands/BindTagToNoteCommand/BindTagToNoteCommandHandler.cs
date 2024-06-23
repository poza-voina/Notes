using MediatR;
using Notes.Core.Interfaces.IServices;

namespace Notes.Api.Tags.Commands;

public class BindTagToNoteCommandHandler : IRequestHandler<BindTagToNoteCommand, ValidatableResponse<int>>
{
    private readonly ITagsService _tagsService;

    public BindTagToNoteCommandHandler(ITagsService tagsService)
    {
        _tagsService = tagsService;
    }
    public async Task<ValidatableResponse<int>> Handle(BindTagToNoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _tagsService.AddTagToNoteAsync(request.TagId!.Value, request.NoteId!.Value);
            return new ValidatableResponse<int> { Result = request.TagId.Value };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<int>
                { ProcessingErrors = new List<ProcessingError> { new ProcessingError { Message = e.Message } } };
        }
    }
}


