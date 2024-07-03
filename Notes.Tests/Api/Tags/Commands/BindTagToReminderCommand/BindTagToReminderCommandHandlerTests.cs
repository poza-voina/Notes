using FluentAssertions;
using Moq;
using Notes.Api.Tags.Commands;
using Notes.Core.Interfaces.IServices;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class BindTagToReminderCommandHandlerTests
{
    private readonly Mock<ITagsService> _mockTagsService = new();
    
    [Fact]
    public async Task Handle_ShouldBeFalse()
    {
        var command = new BindTagToReminderCommand { TagId = 1, ReminderId = 1 };
        _mockTagsService.Setup(m => m.AddTagToReminderAsync(command.TagId.Value, command.ReminderId.Value)).ThrowsAsync(new ArgumentException());
        var result = (await new BindTagToReminderCommandHandler(_mockTagsService.Object).Handle(command, new CancellationToken())).IsValid;
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handle_ShouldTrue()
    {
        var command = new BindTagToReminderCommand() { TagId = 1, ReminderId = 1 };
        _mockTagsService.Setup(m => m.AddTagToNoteAsync(command.TagId.Value, command.ReminderId.Value));
        var result = (await new BindTagToReminderCommandHandler(_mockTagsService.Object).Handle(command, new CancellationToken())).IsValid;
        result.Should().BeTrue();
    }
}