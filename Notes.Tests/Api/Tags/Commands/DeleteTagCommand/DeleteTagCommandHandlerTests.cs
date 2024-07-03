using FluentAssertions;
using Moq;
using Xunit;
using Notes.Api.Tags.Commands;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;

namespace Notes.Tests.Api.Tags.Commands;

public class DeleteTagCommandHandlerTests
{
    private readonly Mock<ITagRepository> _mockTagRepository = new();
    
    [Fact]
    public async Task Handle_ShouldBeFalse()
    {
        var command = new DeleteTagCommand {Id = 1};
        _mockTagRepository.Setup(m => m.GetAsync(command.Id.Value)).ThrowsAsync(new ArgumentException());
        
        var result = (await new DeleteTagCommandHandler(_mockTagRepository.Object).Handle(command, new ())).IsValid;

        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handle_ShouldBeTrue()
    {
        var command = new DeleteTagCommand {Id = 1};
        _mockTagRepository.Setup(m => m.GetAsync(command.Id.Value)).ReturnsAsync(new Tag());
        
        var result = (await new DeleteTagCommandHandler(_mockTagRepository.Object).Handle(command, new ())).IsValid;

        result.Should().BeTrue();
    } 
}