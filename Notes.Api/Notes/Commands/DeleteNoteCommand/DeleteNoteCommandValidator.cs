using FluentValidation;
namespace Notes.Api.Notes.Commands;


public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
