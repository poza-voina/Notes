using FluentAssertions;
using Xunit;
using Notes.Api.Notes.Commands;


namespace Notes.Tests.Api.Notes.Commands;

public class CreateNoteCommandValidatorTests
{
   
    [Fact]
    public void Validate_ShouldReturnFalse_TextIsNullAndTitleIsNull()
    {
        //Arrange
        var createNoteCommand = new CreateNoteCommand();
        //Act
        var validator = new CreateNoteCommandValidator().Validate(createNoteCommand).IsValid;
        //Assert
        validator.Should().BeFalse();
    }
    
    [Fact]
    public void Validate_ShouldReturnTrue_TextIsNullAndTitleIsNonNull()
    {
        //Arrange
        var createNoteCommand = new CreateNoteCommand {Title = "title"};
        //Act
        var validator = new CreateNoteCommandValidator().Validate(createNoteCommand).IsValid;
        //Assert
        validator.Should().BeTrue();
    }
    
    
    [Fact]
    public void Validate_ShouldReturnTrue_TextIsNonNullAndTitleIsNull()
    {
        //Arrange
        var createNoteCommand = new CreateNoteCommand {Text = "text"};
        //Act
        var validator = new CreateNoteCommandValidator().Validate(createNoteCommand).IsValid;
        //Assert
        validator.Should().BeTrue();
    }
    
}