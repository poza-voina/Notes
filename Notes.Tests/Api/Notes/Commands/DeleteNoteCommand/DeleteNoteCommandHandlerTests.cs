using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Notes.Api.Notes.Commands;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;

namespace Notes.Tests.Api.Notes.Commands;

public class DeleteNoteCommandHandlerTests
{
    private readonly Mock<IRepository<Note>> _mockNoteRepostory;

    public DeleteNoteCommandHandlerTests()
    {
        _mockNoteRepostory = new Mock<IRepository<Note>>();
    }
    
    [Fact]
    public async Task Handle_ShouldReturnFalse_NoteIdNotFound()
    {
        var deleteNoteCommand = new DeleteNoteCommand {Id = 1};
        _mockNoteRepostory.Setup(m => m.GetAsync(deleteNoteCommand.Id.Value)).ThrowsAsync(new ArgumentException());
        var result =
            (await new DeleteNoteCommandHandler(_mockNoteRepostory.Object).Handle(deleteNoteCommand,
                new CancellationToken())).IsValid;
        
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handle_ShouldReturnTrue_NoteIdFound()
    {
        var deleteNoteCommand = new DeleteNoteCommand {Id = 1};
        _mockNoteRepostory.Setup(m => m.GetAsync(deleteNoteCommand.Id.Value)).ReturnsAsync(new Note());
        var result =
            (await new DeleteNoteCommandHandler(_mockNoteRepostory.Object).Handle(deleteNoteCommand,
                new CancellationToken())).IsValid;
        
        result.Should().BeTrue();
    }
}