using FluentAssertions;
using Notes.Api.Tags.Commands;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class BindTagToReminderCommandValidatorTests
{
    public static TheoryData<int?, int?> TestData() =>
        new()
        {
            { null, null },
            { 1, null },
            { null, 1 },
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void Validate_ShouldBeFalse(int? tagId, int? reminderId)
    {
        var command = new BindTagToReminderCommand
        {
            TagId = tagId,
            ReminderId = reminderId
        };

        var result = new BindTagToReminderCommandValidator().Validate(command).IsValid;

        result.Should().BeFalse();
    }

    [Fact]
    public void Validate_ShouldBeTrue()
    {
        var command = new BindTagToReminderCommand { TagId = 1, ReminderId = 1 };

        var result = new BindTagToReminderCommandValidator().Validate(command).IsValid;

        result.Should().BeTrue();
    }
    
}