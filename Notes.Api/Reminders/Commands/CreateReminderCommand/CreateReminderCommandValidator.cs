using FluentValidation;
namespace Notes.Api.Reminders.Commands;

public class CreateReminderCommandValidator : AbstractValidator<CreateReminderCommand>
{
    public CreateReminderCommandValidator()
    {
        // RuleFor(cmd => cmd)
        //     .Must(cmd => !(string.IsNullOrEmpty(cmd.Title) && string.IsNullOrEmpty(cmd.Text)));
        
        RuleFor(x => x)
            .Must(x => !(string.IsNullOrEmpty(x.Title) && string.IsNullOrEmpty(x.Text)))
            .WithMessage("Either Title or Text must be provided.");

        RuleFor(x => x.Title)
            .Must((cmd, title) => !string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(cmd.Text))
            .WithMessage("Title can be null only if Text is provided.");

        RuleFor(x => x.Text)
            .Must((cmd, text) => !string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(cmd.Title))
            .WithMessage("Text can be null only if Title is provided.");

        RuleFor(x => x.ReminderTime).NotEmpty();
    }
}