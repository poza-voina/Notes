using System.Data;
using FluentValidation;
namespace Notes.Api.Notes.Commands;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(cmd => cmd).Must(x => x.Text is not null || x.Title is not null || x.TagsTitles is not null);
    }
}