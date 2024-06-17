using FluentValidation;
namespace Notes.Api.Reminders.Queries;

public class GetReminderQueryValidator : AbstractValidator<GetReminderQuery>
{
    public GetReminderQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }    
}