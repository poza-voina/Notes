using System.Data;
using FluentValidation;
using Notes.Api.Reminders.Commands;

namespace Notes.Api.Reminders.Commands;

public class UpdateReminderCommandValidator : AbstractValidator<UpdateReminderCommand>
{
    public UpdateReminderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(cmd => cmd).Must(x => x.Text is not null || x.Title is not null || x.TagsTitles is not null || x.ReminderTime is not null);
        RuleFor(e => e.ReminderTime)
            .GreaterThan(DateTime.Now)
            .WithMessage("Note time must be in the future.");
    }
}