using System.Collections;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
namespace Notes.Infrastructure.Services;

public interface ITagsService
{
    public Task AddTagsToNoteAsync(ICollection<string> tagsTitles, Note note);
    public Task SetTagsAsync<TEntity>(ICollection<string> tagTitles, TEntity entity) where TEntity : ITaggable;
    public Task SetTagsToReminderAsync(ICollection<string> tagTitles, Reminder reminder);
    public Task SetTagsToNoteAsync(ICollection<string> tagTitles, Note note);
    public Task AddTagToNoteAsync(int tagId, int noteId);
    public Task AddTagToReminderAsync(int tagId, int reminderId);

    public Task UnbindTagFromNoteAsync(int tagId, int noteId);
    public Task UnbindTagFromReminderAsync(int tagId, int reminderId);

}

public class TagsService : ITagsService
{
    private readonly IRepository<Note> _noteRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IRepository<Reminder> _reminderRepository;
    

    public TagsService(ITagRepository tagRepository, IRepository<Note> noteRepository, IRepository<Reminder> reminderRepository)
    {
        _tagRepository = tagRepository;
        _noteRepository = noteRepository;
        _reminderRepository = reminderRepository;
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
    
    public async Task SetTagsAsync<TEntity> (ICollection<string> tagTitles, TEntity entity) where TEntity : ITaggable
    {
        ICollection<Tag> allTags = await _tagRepository.CreateTagsAsync(tagTitles);
        if (entity.Tags is null)
        {
            entity.Tags = new List<Tag>();
        }
        entity.Tags = allTags;
    }

    public async Task UnbindTagFromNoteAsync(int tagId, int noteId)
    {
        var note = await _noteRepository.GetAsync(noteId);
        if (note.Tags is null)
        {
            throw new InvalidOperationException("tag not found");
        }
        var tag = note.Tags.First(t => t.Id == tagId);
        note.Tags.Remove(tag);
        await _noteRepository.UpdateAsync(note);
    }
    
    public async Task UnbindTagFromReminderAsync(int tagId, int reminderId)
    {
        var reminder = await _reminderRepository.GetAsync(reminderId);
        if (reminder.Tags is null)
        {
            throw new InvalidOperationException("tag not found");
        }
        var tag = reminder.Tags.First(t => t.Id == tagId);
        reminder.Tags.Remove(tag);
        await _reminderRepository.UpdateAsync(reminder);
    }

    public async Task SetTagsToNoteAsync(ICollection<string> tagTitles, Note note)
    {
        await SetTagsAsync<Note>(tagTitles, note);
    }

    public async Task SetTagsToReminderAsync(ICollection<string> tagTitles, Reminder reminder)
    {
        await SetTagsAsync<Reminder>(tagTitles, reminder);
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

    public async Task AddTagToReminderAsync(int tagId, int reminderId)
    {
        var tag = await _tagRepository.GetAsync(tagId);
        var reminder = await _reminderRepository.GetAsync(reminderId);
        if (reminder.Tags is null)
        {
            reminder.Tags = new List<Tag>();
        }
        reminder.Tags.Add(tag);
        await _reminderRepository.UpdateAsync(reminder);
    }
    
    public void AddTagsToNote(ICollection<Tag> tags, Note note)
    {
        throw new NotImplementedException();
    }
}