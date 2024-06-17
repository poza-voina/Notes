using MediatR;
using Notes.Api.Reminders.ViewModels;
namespace Notes.Api.Reminders.Commands;

public class UpdateReminderCommand : IRequest<ValidatableResponse<ReminderVm>>
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    
    public ICollection<string>? TagsTitles { get; set; }
}