using System.Data;
using FluentValidation;
using Notes.Api.Reminders.Commands;

namespace Notes.Api.Reminders.Commands;

public class UpdateReminderCommandValidator : AbstractValidator<UpdateReminderCommand>
{
    public UpdateReminderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(cmd => cmd).Must(x => x.Text is not null || x.Title is not null);
    }
}