using Notes.Core.Entities;

namespace Notes.Api.Reminders.ViewModels;

public class RemindersVm
{
    public ICollection<Reminder> Reminders { get; init; }

    public RemindersVm()
    {
        Reminders = new List<Reminder>();
    }

    public RemindersVm(ICollection<Reminder> reminders)
    {
        Reminders = reminders;
    }
}