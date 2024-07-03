using FluentAssertions;
using Moq;
using Notes.Api.Tags.Commands;
using Notes.Core.Interfaces.IServices;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class UnbindTagFromNoteCommandHandlerTests
{
    private readonly Mock<ITagsService> _mockTagsService = new();
    
    [Fact]
    public async Task Handle_ShouldBeFalse()
    {
        var command = new UnbindTagFromNoteCommand { TagId = 1, NoteId = 1 };
        _mockTagsService.Setup(m => m.UnbindTagFromNoteAsync(command.TagId.Value, command.NoteId.Value)).ThrowsAsync(new InvalidOperationException());
        var result = (await new UnbindTagFromNoteCommandHandler(_mockTagsService.Object).Handle(command, new CancellationToken())).IsValid;
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handle_ShouldTrue()
    {
        var command = new UnbindTagFromNoteCommand { TagId = 1, NoteId = 1 };
        _mockTagsService.Setup(m => m.UnbindTagFromNoteAsync(command.TagId.Value, command.NoteId.Value));
        var result = (await new UnbindTagFromNoteCommandHandler(_mockTagsService.Object).Handle(command, new CancellationToken())).IsValid;
        result.Should().BeTrue();
    }
}