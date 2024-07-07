using FluentAssertions;
using Notes.Api.Reminders.Commands;
using Npgsql.Replication.TestDecoding;
using Xunit;
namespace Notes.Tests.Api.Reminders.Commands;

public class UpdateReminderCommandValidatorTests
{
    private readonly UpdateReminderCommandValidator _validator = new UpdateReminderCommandValidator();

    public static TheoryData<int?, string?, string?, List<string>?, DateTime?> TestBadData() =>
        new TheoryData<int?, string?, string?, List<string>?, DateTime?>
        {
            { null, null, null, null, null},
            { 1, null, null, null, null},
            { 1, null, null, null, DateTime.Now.AddDays(-1)},
        };
    
    public static TheoryData<int?, string?, string?, List<string>?, DateTime?> TestGoodData() =>
        new TheoryData<int?, string?, string?, List<string>?, DateTime?>
        {
            { 1, "text", null, null , DateTime.Now.AddDays(1)},
            { 1, null, "title", null, DateTime.Now.AddDays(1)},
            { 1, null, null, new List<string> {"tag1"}, DateTime.Now.AddDays(1)},
        };
    
    [Theory]
    [MemberData(nameof(TestBadData))]
    public void Validate_ShouldBeFalse(int? id, string? text, string? title, List<string>? tagTitles, DateTime? dateTime)
    {
        var command = new UpdateReminderCommand()
        {
            Id = id,
            Text = text,
            Title = title,
            TagsTitles = tagTitles,
            ReminderTime = dateTime
        };

        var result = _validator.Validate(command).IsValid;

        result.Should().BeFalse();
    }
    
    
    [Theory]
    [MemberData(nameof(TestGoodData))]
    public void Validate_ShouldBeTrue(int? id, string? text, string? title, List<string>? tagTitles, DateTime? dateTime)
    {
        var command = new UpdateReminderCommand()
        {
            Id = id,
            Text = text,
            Title = title,
            TagsTitles = tagTitles,
            ReminderTime = dateTime
        };

        var result = _validator.Validate(command).IsValid;

        result.Should().BeTrue();
    }
}