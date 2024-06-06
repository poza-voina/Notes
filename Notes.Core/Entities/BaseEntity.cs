using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Core.Entities;

public abstract class BaseEntity
{
    [Column("Id")]
    public int Id { get; set; } = default!;
}