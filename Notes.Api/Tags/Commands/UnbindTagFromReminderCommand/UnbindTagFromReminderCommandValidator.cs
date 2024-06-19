using FluentValidation;
namespace Notes.Api.Tags.Commands;

public class UnbindTagFromReminderCommandValidator : AbstractValidator<UnbindTagFromReminderCommand>
{
    public UnbindTagFromReminderCommandValidator()
    {
        RuleFor(e => e.TagId).NotEmpty();
        RuleFor(e => e.ReminderId).NotEmpty();
    }
}