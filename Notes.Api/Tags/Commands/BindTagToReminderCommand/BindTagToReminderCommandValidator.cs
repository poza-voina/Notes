using FluentValidation;
namespace Notes.Api.Tags.Commands;

public class BindTagToReminderCommandValidator : AbstractValidator<BindTagToReminderCommand>
{
    public BindTagToReminderCommandValidator()
    {
        RuleFor(cmd => cmd).Must(cmd => cmd.TagId is not null && cmd.ReminderId is not null);
    }
}

