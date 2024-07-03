using FluentAssertions;
using Notes.Api.Reminders.Commands;
using Npgsql.Replication.TestDecoding;
using Xunit;
namespace Notes.Tests.Api.Reminders.Commands;

public class UpdateReminderCommandValidatorTests
{
    private readonly UpdateReminderCommandValidator _validator = new UpdateReminderCommandValidator();

    public static TheoryData<int?, string?, string?, List<string>?> TestBadData() =>
        new TheoryData<int?, string?, string?, List<string>?>
        {
            { null, null, null, null },
            { 1, null, null, null },
        };
    
    public static TheoryData<int?, string?, string?, List<string>?> TestGoodData() =>
        new TheoryData<int?, string?, string?, List<string>?>
        {
            { 1, "text", null, null },
            { 1, null, "title", null },
            { 1, null, null, new List<string> {"tag1"} },
        };
    
    [Theory]
    [MemberData(nameof(TestBadData))]
    public void Validate_ShouldBeFalse(int? id, string? text, string? title, List<string>? tagTitles)
    {
        var command = new UpdateReminderCommand()
        {
            Id = id,
            Text = text,
            Title = title,
            TagsTitles = tagTitles
        };

        var result = _validator.Validate(command).IsValid;

        result.Should().BeFalse();
    }
    
    
    [Theory]
    [MemberData(nameof(TestGoodData))]
    public void Validate_ShouldBeTrue(int? id, string? text, string? title, List<string>? tagTitles)
    {
        var command = new UpdateReminderCommand()
        {
            Id = id,
            Text = text,
            Title = title,
            TagsTitles = tagTitles
        };

        var result = _validator.Validate(command).IsValid;

        result.Should().BeTrue();
    }
}