using System.Collections;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
namespace Notes.Infrastructure.Services;

public interface ITagsService
{
    public Task<Note> AddTagsToNoteAsync(ICollection<string> tagsTitles, Note note);
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

    public async Task<Note> AddTagsToNoteAsync(ICollection<string> tagsTitles, Note note)
    {
        ICollection<Tag> allTags = await _tagRepository.CreateTagsAsync(tagsTitles);
        if (note.Tags is null)
        {
            note.Tags = new List<Tag>();
        }
        Console.WriteLine($"note.Tags is null = {note.Tags is null}");
        note.Tags = note.Tags.Union(allTags).ToList();
        Console.WriteLine($"note.Tags is null = {note.Tags is null}");

        return note;
    }
    
    public void AddTagsToNote(ICollection<Tag> tags, Note note)
    {
        throw new NotImplementedException();
    }
}