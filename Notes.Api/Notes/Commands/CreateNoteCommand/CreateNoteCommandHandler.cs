using FluentValidation;
using MediatR;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
using Notes.Infrastructure.Services;
using Notes.Api.Notes.ViewModels;

namespace Notes.Api.Notes.Commands;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, ValidatableResponse<NoteVm>>
{
    private readonly IRepository<Note> _noteRepository;
    private readonly IValidator<CreateNoteCommand> _validator;
    private readonly ITagsService _tagsService;
    public CreateNoteCommandHandler(IRepository<Note> noteRepository, IValidator<CreateNoteCommand> validator, ITagsService tagsService)
    {
        _noteRepository = noteRepository;
        _validator = validator;
        _tagsService = tagsService;
    }
    
    public async Task<ValidatableResponse<NoteVm>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("CreateNoteCommandHandler Handle");
        Note note = new Note { Title = request.Title ?? "", Text = request.Text ?? ""};
        if (request.TagsTitles is not null && request.TagsTitles.Count != 0)
        {
            await _tagsService.SetTagsToNoteAsync(request.TagsTitles, note);
        }
        
        note = await _noteRepository.CreateAsync(note);
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
}