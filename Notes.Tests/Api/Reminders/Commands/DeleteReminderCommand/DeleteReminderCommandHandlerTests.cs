using FluentAssertions;
using Xunit;
using Moq;
using Notes.Api.Reminders.Commands;
using Notes.Core.Interfaces.IRepositories;
using Notes.Core.Entities;

namespace Notes.Tests.Api.Reminders.Commands;

public class DeleteReminderCommandHandlerTests
{
    private readonly Mock<IRepository<Reminder>> _mockReminderRepository = new();

    [Fact]
    public async Task Handle_ShouldBeFalse()
    {
        var command = new DeleteReminderCommand {Id = 1};
        _mockReminderRepository.Setup(m => m.GetAsync(command.Id.Value)).ThrowsAsync(new ArgumentException());
        var result = 
                (await new DeleteReminderCommandHandler(_mockReminderRepository.Object).Handle(command, new CancellationToken())).IsValid;
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handle_ShouldBeTrue()
    {
        var command = new DeleteReminderCommand {Id = 1};
        _mockReminderRepository.Setup(m => m.GetAsync(command.Id.Value)).ReturnsAsync(new Reminder());
        var result = 
            (await new DeleteReminderCommandHandler(_mockReminderRepository.Object).Handle(command, new CancellationToken())).IsValid;
        result.Should().BeTrue();
    }
}