using FluentValidation;
using MediatR;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;

namespace Notes.Api.Notes.Commands;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
{
    private readonly IRepository<Note> _noteRepository;
    private readonly IValidator<CreateNoteCommand> _validator;
    public CreateNoteCommandHandler(IRepository<Note> noteRepository, IValidator<CreateNoteCommand> validator)
    {
        _noteRepository = noteRepository;
        _validator = validator;
    }
    
    public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("CreateNoteCommandHandler Handle");
        // await _validator.ValidateAndThrowAsync(request, cancellationToken);
        // Console.WriteLine("провалидирован")?;
        Note note = await _noteRepository.CreateAsync(new Note {Title = request.Title, Text = request.Text});
        return note.Id;
    }
}