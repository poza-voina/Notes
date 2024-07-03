using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;
using Notes.Api.Reminders.Commands;
using Notes.Core.Entities;

namespace Notes.Tests.Api.Reminders.Commands;

public class DeleteReminderCommandValidatorTests
{
    [Fact]
    public void Validate_ShouldBeFalse()
    {
        var command = new DeleteReminderCommand();
        var result = new DeleteReminderCommandValidator().Validate(command).IsValid;

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Validate_ShouldBeTrue()
    {
        var command = new DeleteReminderCommand{Id = 1};
        var result = new DeleteReminderCommandValidator().Validate(command).IsValid;

        result.Should().BeTrue();
    }
}