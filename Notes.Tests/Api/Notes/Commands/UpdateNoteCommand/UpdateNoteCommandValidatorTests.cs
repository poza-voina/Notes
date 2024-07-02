using FluentAssertions;
using Xunit;
using Notes.Api.Notes.Commands;


namespace Notes.Tests.Api.Notes.Commands;

public class UpdateNoteValidatorTests
{
    [Fact]
    public void Validate_ShouldReturnFalse_IdIsNullTextIsNullAndTitleIsNullAndTagsTitlesIsNull()
    {
        var command = new UpdateNoteCommand();
        var validator = new UpdateNoteCommandValidator();

        var result = validator.Validate(command).IsValid;

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Validate_ShouldReturnTrue_IdIsNonNullTextIsNonNullAndTitleIsNullAndTagsTitlesIsNull()
    {
        var command = new UpdateNoteCommand {Id = 0, Text = "text"};
        var validator = new UpdateNoteCommandValidator();

        var result = validator.Validate(command).IsValid;

        result.Should().BeTrue();
    }
    
    [Fact]
    public void Validate_ShouldReturnTrue_IdIsNonNullTextIsNullAndTitleIsNonNullAndTagsTitlesIsNull()
    {
        var command = new UpdateNoteCommand {Id = 0, Title = "title"};
        var validator = new UpdateNoteCommandValidator();

        var result = validator.Validate(command).IsValid;

        result.Should().BeTrue();
    }
    [Fact]
    public void Validate_ShouldReturnTrue_IdIsNonNullTextIsNullAndTitleIsNullAndTagsTitlesIsNonNull()
    {
        var command = new UpdateNoteCommand {Id = 0, TagsTitles = new List<string> {"tag1"}};
        var validator = new UpdateNoteCommandValidator();

        var result = validator.Validate(command).IsValid;

        result.Should().BeTrue();
    }
}