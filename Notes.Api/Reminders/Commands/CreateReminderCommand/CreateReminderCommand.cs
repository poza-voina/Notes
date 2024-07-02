using MediatR;
using Notes.Api;
using Notes.Api.Reminders.ViewModels;

namespace Notes.Api.Reminders.Commands;


public class CreateReminderCommand : IRequest<ValidatableResponse<ReminderVm>>
{
    public string? Title { get; set; }
    public string? Text { get; set; }
    
    public DateTime? ReminderTime { get; set; }
    
    public ICollection<string>? TagsTitles { get; set; } 
}