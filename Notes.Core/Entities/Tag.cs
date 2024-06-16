using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Core.Entities;

[Table("Tags")]
public class Tag : BaseEntity
{
    [Column("Title")]
    public string Title { get; set; } = default!;

    public virtual ICollection<Note>? Notes { get; set; } = default!;
    public virtual ICollection<Reminder>? Reminders { get; set; } = default!;
}