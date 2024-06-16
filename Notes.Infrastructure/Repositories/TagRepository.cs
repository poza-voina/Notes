using System.Collections;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
namespace Notes.Infrastructure.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    public Task<ICollection<Tag>> CreateTagsAsync(ICollection<string> tagsTitles);

    public Task<ICollection<Tag>> CreateNonExistingTagsAsync(IQueryable<Tag> allTags);
}


public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Tag>> CreateTagsAsync(ICollection<string> tagsTitles)
    {
        ICollection<Tag> allTags = tagsTitles.Select(item => new Tag {Title = item}).Distinct().ToList();
        await CreateNonExistingTagsAsync(allTags.AsQueryable());
        return allTags;
    }
    
    public async Task<ICollection<Tag>> CreateNonExistingTagsAsync(IQueryable<Tag> allTags)
    {
        ICollection<Tag> nonExistingTags = allTags.Where(tag => !DbContext.Tags.Any(item => item.Title == tag.Title)).ToList();
        foreach (var tag in nonExistingTags)
        {
            DbContext.Add(tag);
        }
        await DbContext.SaveChangesAsync();
        return nonExistingTags;
    } 
}