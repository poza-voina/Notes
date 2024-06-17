using System.Collections;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
namespace Notes.Infrastructure.Services;

public interface ITagsService
{
    public Task AddTagsToNoteAsync(ICollection<string> tagsTitles, Note note);
    public Task SetTagsToNoteAsync(ICollection<string> tagTitles, Note note);
    public Task AddTagToNoteAsync(int tagId, int noteId);
}

public class TagsService : ITagsService
{
    private readonly IRepository<Note> _noteRepository;
    private readonly ITagRepository _tagRepository;

    public TagsService(ITagRepository tagRepository, IRepository<Note> noteRepository)
    {
        _tagRepository = tagRepository;
        _noteRepository = noteRepository;
    }

    public async Task AddTagsToNoteAsync(ICollection<string> tagsTitles, Note note)
    {
        ICollection<Tag> allTags = await _tagRepository.CreateTagsAsync(tagsTitles);
        if (note.Tags is null)
        {
            note.Tags = new List<Tag>();
        }
        Console.WriteLine($"note.Tags is null = {note.Tags is null}");
        note.Tags = note.Tags.Union(allTags).ToList();
        Console.WriteLine($"note.Tags is null = {note.Tags is null}");
    }

    public async Task SetTagsToNoteAsync(ICollection<string> tagTitles, Note note)
    {
        ICollection<Tag> allTags = await _tagRepository.CreateTagsAsync(tagTitles);
        if (note.Tags is null)
        {
            note.Tags = new List<Tag>();
        }
        note.Tags = allTags;
    }

    public async Task AddTagToNoteAsync(int tagId, int noteId)
    {
        var tag = await _tagRepository.GetAsync(tagId);
        var note = await _noteRepository.GetAsync(noteId);
        if (note.Tags is null)
        {
            note.Tags = new List<Tag>();
        }
        note.Tags.Add(tag);
        await _noteRepository.UpdateAsync(note);
    }
    
    public void AddTagsToNote(ICollection<Tag> tags, Note note)
    {
        throw new NotImplementedException();
    }
}