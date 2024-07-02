using System.Collections;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;

namespace Notes.Infrastructure.Repositories;



public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Tag>> CreateTagsAsync(ICollection<string> tagsTitles)
    {
        IEnumerable<Tag> existing;
        IEnumerable<Tag> nonExisting;
        (existing, nonExisting) = await CheckTagsTitlesAsync(tagsTitles);
        await CreateNonExistingTagsAsync(nonExisting);
        return existing.Concat(nonExisting).ToList();
    }

    public async Task<(IEnumerable<Tag> existing, IEnumerable<Tag> nonExisting)> CheckTagsTitlesAsync(ICollection<string> tagsTitles)
    {
        IEnumerable<Tag> existing = Items
            .Where(tag => tagsTitles.Any(tagTitle => tag.Title == tagTitle))
            .ToList();
        Console.WriteLine(Items.Count());
        IEnumerable<Tag> nonExisting = tagsTitles
            .Where(tagTitle => existing.All(tag => tagTitle != tag.Title))
            .Select(item => new Tag {Title = item});

        return (existing, nonExisting);
    }
    
    public async Task CreateNonExistingTagsAsync(IEnumerable<Tag> nonExistingTags)
    {
        foreach (var tag in nonExistingTags)
        {
            DbContext.Add(tag);
        }
        await DbContext.SaveChangesAsync();
    }

    public async Task<Tag> GetTagByTitleAsync(string title) =>
        await Set.FirstOrDefaultAsync(tag => tag.Title == title) ?? throw new ArgumentException($"Entity with id = {title} not found.");
}