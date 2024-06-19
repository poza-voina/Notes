using Notes.Core.Entities;

namespace Notes.Api.Notes.ViewModels;

public class NotesVm
{
    public ICollection<Note> Notes { get; init; }

    public NotesVm()
    {
        Notes = new List<Note>();
    }

    public NotesVm(ICollection<Note> notes)
    {
        Notes = notes;
    }
}