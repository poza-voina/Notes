using FluentAssertions;
using Moq;
using Notes.Api.Tags.Commands;
using Notes.Core.Interfaces.IServices;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class UnbindTagFromReminderCommandHandlerTests
{
    private readonly Mock<ITagsService> _mockTagsService = new();
    
    [Fact]
    public async Task Handle_ShouldBeFalse()
    {
        var command = new UnbindTagFromReminderCommand { TagId = 1, ReminderId = 1 };
        _mockTagsService.Setup(m => m.UnbindTagFromReminderAsync(command.TagId.Value, command.ReminderId.Value)).ThrowsAsync(new InvalidOperationException());
        var result = (await new UnbindTagFromReminderCommandHandler(_mockTagsService.Object).Handle(command, new CancellationToken())).IsValid;
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handle_ShouldTrue()
    {
        var command = new UnbindTagFromReminderCommand { TagId = 1, ReminderId = 1 };
        _mockTagsService.Setup(m => m.UnbindTagFromReminderAsync(command.TagId.Value, command.ReminderId.Value));
        var result = (await new UnbindTagFromReminderCommandHandler(_mockTagsService.Object).Handle(command, new CancellationToken())).IsValid;
        result.Should().BeTrue();
    }
}