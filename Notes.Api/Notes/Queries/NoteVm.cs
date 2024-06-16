using Notes.Core.Entities;

namespace Notes.Api.Notes.Queries;

public class NoteVm
{
    public int Id { get; init; } = default!;
    public string? Title { get; init; }
    public string? Text { get; init; }
    
    public ICollection<Tag>? Tags { get; set; }
}