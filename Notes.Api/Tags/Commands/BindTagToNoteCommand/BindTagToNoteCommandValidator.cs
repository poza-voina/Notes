using FluentValidation;
namespace Notes.Api.Tags.Commands;

public class BindTagToNoteCommandValidator : AbstractValidator<BindTagToNoteCommand>
{
    public BindTagToNoteCommandValidator()
    {
        RuleFor(cmd => cmd).Must(cmd => cmd.TagId is not null && cmd.NoteId is not null);
    }
}

