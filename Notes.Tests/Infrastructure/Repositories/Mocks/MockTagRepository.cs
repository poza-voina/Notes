using Moq;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Infrastructure.Repositories;

namespace Notes.Tests.Infrastructure.Repositories.Mocks;

public static class MockTagRepository
{
    public static Mock<TagRepository> GetMock()
    {
        var mock = new Mock<TagRepository>();

        Tag[] tags = new[]
        {
            new Tag
            {
                Id = 1,
                Title = "tag2",
            },
            new Tag
            {
                Id = 2,
                Title = "tag3",
            },
            new Tag
            {
                Id = 3,
                Title = "tag4",
            }
        };


        mock.Setup(m => m.CreateTagsAsync(It.IsAny<List<string>>()))
            .Callback(() => {return;});

        mock.Setup(m => m.CreateNonExistingTagsAsync(It.IsAny<List<Tag>>()))
            .Callback(() => {return;});
        
        
        
        return mock;
    }
}