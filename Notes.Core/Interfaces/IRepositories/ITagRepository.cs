using Notes.Core.Entities;
namespace Notes.Core.Interfaces.IRepositories;

public interface ITagRepository : IRepository<Tag>
{
    public Task<ICollection<Tag>> CreateTagsAsync(ICollection<string> tagsTitles);

    public Task<ICollection<Tag>> CreateNonExistingTagsAsync(IQueryable<Tag> allTags);
}
