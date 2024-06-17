using MediatR;

namespace Notes.Api.Reminders.Commands;

public class DeleteReminderCommand : IRequest<ValidatableResponse<int>>
{
    public int? Id { get; set; }
}