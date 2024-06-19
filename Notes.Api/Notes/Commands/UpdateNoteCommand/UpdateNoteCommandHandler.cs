using System.Diagnostics;
using MediatR;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
using Notes.Infrastructure.Services;
using Notes.Api.Notes.ViewModels;

namespace Notes.Api.Notes.Commands;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, ValidatableResponse<NoteVm>>
{
    private readonly IRepository<Note> _repository;
    private readonly ITagsService _tagsService;

    public UpdateNoteCommandHandler(IRepository<Note> repository, ITagsService tagsService)
    {
        _repository = repository;
        _tagsService = tagsService;
    }


    public async Task<ValidatableResponse<NoteVm>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
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
            return new ValidatableResponse<NoteVm>
            {
                Result = new NoteVm
                {
                    Id = note.Id,
                    Title = note.Title,
                    Text = note.Text,
                    Tags = note.Tags
                }
            };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<NoteVm>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError { Message = e.Message } }
            };
        }
    }
}