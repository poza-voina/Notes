using FluentValidation;
namespace Notes.Api.Reminders.Commands;


public class DeleteReminderCommandValidator : AbstractValidator<DeleteReminderCommand>
{
    public DeleteReminderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
