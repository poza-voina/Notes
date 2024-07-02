using FluentAssertions;
using Xunit;
using Moq;
using Notes.Core.Interfaces.IRepositories;
using Notes.Core.Interfaces.IServices;
using Notes.Core.Entities;
using Notes.Api.Notes.Commands;

namespace Notes.Tests.Api.Notes.Commands;

public class UpdateNoteCommandHandlerTests
{
    private readonly Mock<IRepository<Note>> _mockNoteRepostory;
    private readonly Mock<ITagsService> _mockNoteService;

    public UpdateNoteCommandHandlerTests()
    {
        _mockNoteRepostory = new Mock<IRepository<Note>>();
        _mockNoteService = new Mock<ITagsService>();
    }
    
    [Fact]
    public async Task Handle_ShouldReturnFalse_NoteIdNotFound()
    {
        var updateNoteCommand = new UpdateNoteCommand {Id = 1};
        _mockNoteRepostory.Setup(m => m.GetAsync(updateNoteCommand.Id.Value)).ThrowsAsync(new ArgumentException());
        _mockNoteService.Setup(m => m.SetTagsToNoteAsync(new List<string> { "tag1" }, new Note()));
        var result =
            (await new UpdateNoteCommandHandler(_mockNoteRepostory.Object, _mockNoteService.Object).Handle(updateNoteCommand,
                new CancellationToken())).IsValid;
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handle_ShouldReturnFalse_NoteIdFound()
    {
        var updateNoteCommand = new UpdateNoteCommand {Id = 1};
        _mockNoteRepostory.Setup(m => m.GetAsync(updateNoteCommand.Id.Value)).ReturnsAsync(new Note());
        _mockNoteService.Setup(m => m.SetTagsToNoteAsync(new List<string> { "tag1" }, new Note()));
        var result =
            (await new UpdateNoteCommandHandler(_mockNoteRepostory.Object, _mockNoteService.Object).Handle(updateNoteCommand,
                new CancellationToken())).IsValid;
        
        result.Should().BeTrue();
    }
    
}