using Notes.Core.Entities;
namespace Notes.Core.Interfaces.IServices;

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