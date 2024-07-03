using FluentAssertions;
using Notes.Api.Tags.Commands;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class CreateTagCommandValidatorTests
{
    [Fact]
    public void Validate_ShouldBeFalse()
    {
        var command = new CreateTagCommand();
        
        var result = new CreateTagCommandValidator().Validate(command).IsValid;

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Validate_ShouldBeTrue()
    {
        var command = new CreateTagCommand {Title = "title"};
        
        var result = new CreateTagCommandValidator().Validate(command).IsValid;

        result.Should().BeTrue();
    }
}