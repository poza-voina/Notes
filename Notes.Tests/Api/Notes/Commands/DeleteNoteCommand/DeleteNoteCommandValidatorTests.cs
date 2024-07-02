using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Notes.Api.Notes.Commands;

namespace Notes.Tests.Api.Notes.Commands;

public class DeleteNoteCommandValidatorTests
{
    [Fact]
    public void Validate_ShouldReturnFalse_IdIsNull()
    {
        //Arrange
        var deleteNoteCommand = new DeleteNoteCommand();
        var deleteNoteCommandValidator = new DeleteNoteCommandValidator();
        //Act
        var result = deleteNoteCommandValidator.Validate(deleteNoteCommand).IsValid;
        //Assert
        result.Should().BeFalse();
    }
 
    [Fact]
    public void Validate_ShouldReturnTrue_IdIsNonNull()
    {
        //Arrange
        var deleteNoteCommand = new DeleteNoteCommand {Id = 1};
        var deleteNoteCommandValidator = new DeleteNoteCommandValidator();
        //Act
        var result = deleteNoteCommandValidator.Validate(deleteNoteCommand).IsValid;
        //Assert
        result.Should().BeTrue();
    }

    
}