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

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Tag))
        {
            return false;
        }

        var other = (Tag)obj;
        return this.Title == other.Title;
    }
}