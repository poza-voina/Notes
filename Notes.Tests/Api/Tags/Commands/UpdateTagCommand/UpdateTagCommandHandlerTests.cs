using FluentAssertions;
using Notes.Core.Interfaces.IRepositories;
using Moq;
using Notes.Api;
using Notes.Api.Tags.Commands;
using Notes.Core.Entities;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class UpdateTagCommandHandlerTests
{
    private readonly Mock<ITagRepository> _mockTagRepository = new();

    [Fact]
    public async Task Handle_ShouldBeNotFound()
    {
        var command = new UpdateTagCommand { Id = 1, Title = "string" };
        _mockTagRepository.Setup(m => m.GetAsync(command.Id.Value)).ThrowsAsync(new ArgumentException());

        var result =
        (
            await new UpdateTagCommandHandler(_mockTagRepository.Object).Handle(command, new CancellationToken())
        ).ProcessingErrors.First().Type;

        result.Should().Be(ProcessingErrors.NotFound);
    }
    
    
    
    [Fact]
    public async Task Handle_ShouldBeConflict()
    {
        var command = new UpdateTagCommand { Id = 1, Title = "string" };
        var tag = new Tag { Id = 1, Title = "string" };
        _mockTagRepository.Setup(m => m.GetAsync(command.Id.Value)).ReturnsAsync(tag);
        _mockTagRepository.Setup(m => m.GetTagByTitleOrDefaultAsync(command.Title)).ReturnsAsync(tag);
        
        var result =
        (
            await new UpdateTagCommandHandler(_mockTagRepository.Object).Handle(command, new CancellationToken())
        ).ProcessingErrors.First().Type;

        result.Should().Be(ProcessingErrors.Conflict);
    }
    
    
    [Fact]
    public async Task Handle_ShouldBeTrue()
    {
        var command = new UpdateTagCommand { Id = 1, Title = "string" };
        var tag = new Tag { Id = 1, Title = "string" };
        _mockTagRepository.Setup(m => m.GetAsync(command.Id.Value)).ReturnsAsync(tag);
        _mockTagRepository.Setup(m => m.GetTagByTitleOrDefaultAsync(command.Title)).ReturnsAsync((Tag?)null);
        _mockTagRepository.Setup(m => m.UpdateAsync(tag)).ReturnsAsync(tag);
        
        var result =
        (
            await new UpdateTagCommandHandler(_mockTagRepository.Object).Handle(command, new CancellationToken())
        ).IsProcessingValid;

        result.Should().BeTrue();
    }
}