using FluentAssertions;
using Notes.Api.Tags.Commands;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class UnbindTagFromReminderCommandValidatorTests
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
        var command = new UnbindTagFromReminderCommand
        {
            TagId = tagId,
            ReminderId = reminderId
        };

        var result = new UnbindTagFromReminderCommandValidator().Validate(command).IsValid;

        result.Should().BeFalse();
    }

    [Fact]
    public void Validate_ShouldBeTrue()
    {
        var command = new UnbindTagFromReminderCommand { TagId = 1, ReminderId = 1 };

        var result = new UnbindTagFromReminderCommandValidator().Validate(command).IsValid;

        result.Should().BeTrue();
    }
    
}