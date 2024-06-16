using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Core.Entities;


[Table("Notes")]
public class Note : BaseEntity
{
    [Column("Title")]
    public string Title { get; set; } = default!;
    [Column("Text")]
    public string Text { get; set; } = default!;

    public virtual ICollection<Tag>? Tags { get; set; }

}