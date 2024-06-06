using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Core.Entities;

[Table("Tags")]
public class Tag : BaseEntity
{
    [Column("Title")]
    public string Title { get; set; } = default!;
}