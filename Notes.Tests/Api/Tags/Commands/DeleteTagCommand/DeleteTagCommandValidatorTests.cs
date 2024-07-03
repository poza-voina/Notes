using FluentAssertions;
using Notes.Api.Tags.Commands;
using Xunit;

namespace Notes.Tests.Api.Tags.Commands;

public class CreateTagCommandHandlerTests
{
    [Fact]
    public void Validate_ShouldBeFalse()
    {
        var command = new DeleteTagCommand();

        var result = new DeleteTagCommandValidator().Validate(command).IsValid;

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Validate_ShouldBeTrue()
    {
        var command = new DeleteTagCommand {Id = 1};

        var result = new DeleteTagCommandValidator().Validate(command).IsValid;

        result.Should().BeTrue();
    }
}