using FluentAssertions;
using Xunit;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
using Notes.Tests.Mocks;

namespace Notes.Tests.Infrastructure.Repositories;

public class TagRepositoryTests
{
    [Theory]
    [MemberData(nameof(GetTestData))]
    public async Task
        CheckTagsTitlesAsync_ExistingTagShouldBeEquivalentToAllegedExistingDataAndNonExistingTagShouldBeEquivalentToAllegedNonExistingData(
            List<Tag> startData, List<string> addedData, List<Tag> allegedExistingData,
            List<Tag> allegedNonExistingData)
    {
        var mockApplicationDbContext = new MockApplicationDbContext()
            .SetEntities<Tag>(startData)
            .MockedContext;

        TagRepository tagRepository = new TagRepository(mockApplicationDbContext.Object);

        IEnumerable<Tag> existing;
        IEnumerable<Tag> nonExisting;

        (existing, nonExisting) = await tagRepository.CheckTagsTitlesAsync(addedData);
        nonExisting.Should().BeEquivalentTo(allegedNonExistingData);
        existing.Should().BeEquivalentTo(allegedExistingData);
    }

    public static TheoryData<List<Tag>, List<string>, List<Tag>, List<Tag>> GetTestData =>
        new()
        {
            {
                //Start data
                new List<Tag>
                {
                    new Tag { Id = 1, Title = "tag1" },
                    new Tag { Id = 2, Title = "tag2" },
                    new Tag { Id = 3, Title = "tag3" },
                },
                //Added data
                new List<string>
                {
                    "tag1",
                    "tag2",
                    "tag3",
                    "tag4",
                },
                //Existing data
                new List<Tag>
                {
                    new Tag { Id = 1, Title = "tag1" },
                    new Tag { Id = 2, Title = "tag2" },
                    new Tag { Id = 3, Title = "tag3" },
                },
                //NonExisting data
                new List<Tag>
                {
                    new Tag { Id = 1, Title = "tag4" }
                }
            },
            {
                //Start data
                new List<Tag>
                {
                    new Tag { Id = 1, Title = "tag1" },
                    new Tag { Id = 2, Title = "tag2" },
                    new Tag { Id = 3, Title = "tag3" },
                },
                //Added data
                new List<string>
                {
                    "tag1",
                    "tag2",
                    "tag3",
                },
                //Existing data
                new List<Tag>
                {
                    new Tag { Id = 1, Title = "tag1" },
                    new Tag { Id = 2, Title = "tag2" },
                    new Tag { Id = 3, Title = "tag3" },
                },
                //NonExisting data
                new List<Tag>()
            },
            {
                //Start data
                new List<Tag>(),
                //Added data
                new List<string>
                {
                    "tag1",
                    "tag2",
                    "tag3",
                    "tag4",
                },
                //Existing data
                new List<Tag>(),
                //NonExisting data
                new List<Tag>
                {
                    new Tag { Id = 1, Title = "tag1" },
                    new Tag { Id = 2, Title = "tag2" },
                    new Tag { Id = 3, Title = "tag3" },
                    new Tag { Id = 4, Title = "tag4" },
                }
            }
        };
}