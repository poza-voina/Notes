using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Notes.Core.Entities;

[Table("Tags")]
public class Tag : BaseEntity
{
    [Column("Title")]
    public string Title { get; set; } = default!;

    [JsonIgnore]
    public virtual ICollection<Note>? Notes { get; set; } = default!;
    
    [JsonIgnore]
    public virtual ICollection<Reminder>? Reminders { get; set; } = default!;
}