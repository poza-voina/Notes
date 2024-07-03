using FluentAssertions;
using Notes.Api.Tags.Commands;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class UnbindTagFromNoteCommandValidatorTests
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
        var command = new UnbindTagFromNoteCommand
        {
            TagId = tagId,
            NoteId = noteId
        };

        var result = new UnbindTagFromNoteCommandValidator().Validate(command).IsValid;

        result.Should().BeFalse();
    }

    [Fact]
    public void Validate_ShouldBeTrue()
    {
        var command = new UnbindTagFromNoteCommand { TagId = 1, NoteId = 1 };

        var result = new UnbindTagFromNoteCommandValidator().Validate(command).IsValid;

        result.Should().BeTrue();
    }
    
}