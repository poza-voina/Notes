using MediatR;
using Notes.Api.Reminders.ViewModels;
namespace Notes.Api.Reminders.Queries;

public class GetReminderQuery : IRequest<ValidatableResponse<ReminderVm>>
{
    public int Id { get; set; }
}