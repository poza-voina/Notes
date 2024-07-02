using FluentAssertions;
using Xunit;
using Notes.Api.Notes.Commands;


namespace Notes.Tests.Api.Notes.Commands;

public class UpdateNoteValidatorTests
{
   
    [Fact]
    public void Validate_ShouldReturnFalse_TextIsNullAndTitleIsNullAndTagsTitlesIsNull()
    {
        var command = new UpdateNoteCommand();
        var validator = new UpdateNoteCommandValidator();

        var result = validator.Validate(command).IsValid;

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Validate_ShouldReturnTrue_TextIsNonNullAndTitleIsNullAndTagsTitlesIsNull()
    {
        var command = new UpdateNoteCommand {TagsTitles = new List<string> {"tag1"}};
        var validator = new UpdateNoteCommandValidator();

        var result = validator.Validate(command).IsValid;

        result.Should().BeTrue();
    }
}