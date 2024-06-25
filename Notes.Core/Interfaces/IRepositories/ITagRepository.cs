using System.Collections;
using Notes.Core.Entities;
namespace Notes.Core.Interfaces.IRepositories;

public interface ITagRepository : IRepository<Tag>
{
    public Task<ICollection<Tag>> CreateTagsAsync(ICollection<string> tagsTitles);

    public Task CreateNonExistingTagsAsync(IEnumerable<Tag> nonExistingTags);
}
