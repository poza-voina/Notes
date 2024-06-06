using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Core.Entities;


[Table("Notes")]
public class Note : BaseEntity
{
    [Column("Title")]
    public string Title { get; set; } = default!;
    [Column("Text")]
    public string Text { get; set; } = default!;
    
    public ICollection<Tag> Tags { get; set; } = default!;
    
}