using FluentAssertions;
using Moq;
using Notes.Api.Tags.Commands;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Xunit;

namespace Notes.Tests.Api.Tags;

public class CreateTagCommandHandlerTests
{
    [Fact]
    public async Task Handler_ShouldBeFalse()
    {
        var command = new CreateTagCommand {Title = "string"};
        var mock = new Mock<ITagRepository>();
        var result = await new CreateTagCommandHandler(mock.Object).Handle(command, new CancellationToken());
        result.IsValid.Should().BeFalse();
    }
    
    
    [Fact]
    public async Task Handler_ShouldBeTrue()
    {
        var command = new CreateTagCommand {Title = "string"};
        var mock = new Mock<ITagRepository>();
        mock.Setup(m => m.GetTagByTitleAsync(command.Title)).ThrowsAsync(new ArgumentException());
        var tag = new Tag { Id = 1, Title = "string" };
        mock.Setup(m => m.CreateAsync(tag)).ReturnsAsync(tag);
        var result = await new CreateTagCommandHandler(mock.Object).Handle(command, new CancellationToken());
        result.IsValid.Should().BeTrue();
    }
}