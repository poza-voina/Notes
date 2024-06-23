using MediatR;
using Notes.Core.Interfaces.IServices;

namespace Notes.Api.Tags.Commands;

public class UnbindTagFromNoteCommandHandler : IRequestHandler<UnbindTagFromNoteCommand, ValidatableResponse<int>>
{
    private readonly ITagsService _tagsService;
    public UnbindTagFromNoteCommandHandler(ITagsService tagsService)
    {
        _tagsService = tagsService;
    }

    public async Task<ValidatableResponse<int>> Handle(UnbindTagFromNoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _tagsService.UnbindTagFromNoteAsync(request.TagId!.Value, request.NoteId!.Value);
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