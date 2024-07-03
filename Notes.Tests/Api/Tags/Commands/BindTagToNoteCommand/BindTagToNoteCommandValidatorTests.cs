using FluentAssertions;
using Notes.Api.Tags.Commands;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class BindTagToNoteCommandValidatorTests
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
    public void Validate_ShouldBeFalse(int? tagId, int? noteId)
    {
        var command = new BindTagToNoteCommand
        {
            TagId = tagId,
            NoteId = noteId
        };

        var result = new BindTagToNoteCommandValidator().Validate(command).IsValid;

        result.Should().BeFalse();
    }

    [Fact]
    public void Validate_ShouldBeTrue()
    {
        var command = new BindTagToNoteCommand { TagId = 1, NoteId = 1 };

        var result = new BindTagToNoteCommandValidator().Validate(command).IsValid;

        result.Should().BeTrue();
    }
    
}