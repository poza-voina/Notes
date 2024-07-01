using FluentAssertions;
using Xunit;
using Notes.Api.Notes.Commands;

namespace Notes.Tests.Api.Notes.Commands;

public class CreateNoteCommandTests
{
    [Fact]
    public void Validate_ShouldReturnSomething_TextIsNullAndTitleIsNull()
    {
        //Arrange
        var createNoteCommand = new CreateNoteCommand();
        var createNoteCommandValidator = new CreateNoteCommandValidator();
        //Act
        var result = createNoteCommandValidator.Validate(createNoteCommand).IsValid;
        //Assert
        result.Should().BeFalse();
    }
    
}