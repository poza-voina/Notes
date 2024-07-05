using FluentAssertions;
using Xunit;
using Moq;
using Notes.Core.Interfaces.IRepositories;
using Notes.Core.Entities;
using Notes.Core.Services;

namespace Notes.Tests.Core.Services;

public class TagsServiceTests
{
    private readonly Mock<IRepository<Note>> _mockNoteRepository = new();
    private readonly Mock<ITagRepository> _mockTagRepository = new();
    private readonly Mock<IRepository<Reminder>> _mockReminderRepository = new();

    [Theory]
    [MemberData(nameof(GetTestData))]
    public async Task SetTagsAsync(ITaggable taggableEntity, List<string>tagTitles)
    {
        var tags = tagTitles.Select(t => new Tag { Title = t }).ToList();
        _mockTagRepository.Setup(m => m.CreateTagsAsync(tagTitles))
            .ReturnsAsync(tags);

        await new TagsService(_mockTagRepository.Object, _mockNoteRepository.Object, _mockReminderRepository.Object)
            .SetTagsAsync(tagTitles, taggableEntity);

        taggableEntity.Tags.Should().BeEquivalentTo(tags);
    }

    public static TheoryData<ITaggable, List<string>> GetTestData() => new()
    {
        { new Note(), new List<string> { "tag1", "tag2", "tag3" } },
        { new Note(), new List<string>() },
        { new Reminder(), new List<string> { "tag1", "tag2", "tag3" } },
        { new Reminder(), new List<string>() }
    };

}