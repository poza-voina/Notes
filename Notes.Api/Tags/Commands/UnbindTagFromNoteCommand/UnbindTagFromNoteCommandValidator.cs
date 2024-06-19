using FluentValidation;
namespace Notes.Api.Tags.Commands;

public class UnbindTagFromNoteCommandValidator : AbstractValidator<UnbindTagFromNoteCommand>
{
    public UnbindTagFromNoteCommandValidator()
    {
        RuleFor(e => e.TagId).NotEmpty();
        RuleFor(e => e.NoteId).NotEmpty();
    }
}