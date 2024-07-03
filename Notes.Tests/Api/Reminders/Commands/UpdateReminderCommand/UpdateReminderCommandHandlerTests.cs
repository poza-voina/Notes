using System.ComponentModel;
using Notes.Api.Reminders.Commands;
using Xunit;
using Moq;
using FluentAssertions;
using Notes.Core.Interfaces.IRepositories;
using Notes.Core.Interfaces.IServices;
using Notes.Core.Entities;

namespace Notes.Tests.Api.Reminders.Commands;


public class UpdateReminderCommandHandlerTests
{
    private readonly Mock<ITagsService> _mockTagsService = new Mock<ITagsService>();
    private readonly Mock<IRepository<Reminder>> _mockReminderRepository = new Mock<IRepository<Reminder>>();
    
    [Fact]
    public async Task Handle_ShouldBeFalse()
    {
        _mockReminderRepository.Setup(m => m.GetAsync(1)).ThrowsAsync(new ArgumentException());
        var command = new UpdateReminderCommand
        {
            Id = 1,
            Title = "title",
            Text = "text",
        };

        var result = 
            (
            await new UpdateReminderCommandHandler(_mockReminderRepository.Object, _mockTagsService.Object)
                .Handle(command, new CancellationToken())
            ).IsValid;

        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handle_ShouldBeTrue()
    {
        _mockReminderRepository.Setup(m => m.GetAsync(1)).ReturnsAsync(new Reminder());
        var command = new UpdateReminderCommand
        {
            Id = 1,
            Title = "title",
            Text = "text",
        };

        var result = 
        (
            await new UpdateReminderCommandHandler(_mockReminderRepository.Object, _mockTagsService.Object)
                .Handle(command, new CancellationToken())
        ).IsValid;

        result.Should().BeTrue();
    }
}