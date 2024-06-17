using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Core.Entities;

[Table("Reminders")]
public class Reminder : BaseEntity, ITaggable
{
    public string Title { get; set; } = default!;
    public string Text { get; set; } = default!;
    public DateTime ReminderTime { get; set; } = default!;

    public virtual ICollection<Tag>? Tags { get; set; }
}