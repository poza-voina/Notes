using System.Diagnostics;
using MediatR;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
using Notes.Infrastructure.Services;

namespace Notes.Api.Notes.Commands;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, ValidatableResponse<int>>
{
    private readonly IRepository<Note> _repository;
    private readonly ITagsService _tagsService;

    public UpdateNoteCommandHandler(IRepository<Note> repository, ITagsService tagsService)
    {
        _repository = repository;
        _tagsService = tagsService;
    }


    public async Task<ValidatableResponse<int>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var note = await _repository.GetAsync(request.Id!.Value);
            note.Text = request.Text ?? note.Text;
            note.Title = request.Title ?? note.Title;
            if (request.TagsTitles is not null)
            {
                await _tagsService.SetTagsToNoteAsync(request.TagsTitles, note);
            }
            await _repository.UpdateAsync(note);
            return new ValidatableResponse<int> { Result = note.Id };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<int>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError { Message = e.Message } }
            };
        }
    }
}