using FluentAssertions;
using Notes.Api.Tags.Commands;
using Xunit;
namespace Notes.Tests.Api.Tags.Commands;

public class UpdateTagCommandValidatorTests
{
    public static TheoryData<int?, string?> TestBadData() =>
        new ()
        {
            { null, null },
            { null, "title" },
            { 1, null }
        };

    [Theory]
    [MemberData(nameof(TestBadData))]
    public void Validate_ShouldBeFalse(int? id, string? title)
    {
        var command = new UpdateTagCommand
        {
            Id = id,
            Title = title
        };
        var result = new UpdateTagCommandValidator().Validate(command).IsValid;

        result.Should().BeFalse();
    }

    [Fact]
    public void Validate_ShouldBeTrue()
    {
        var command = new UpdateTagCommand
        {
            Id = 1,
            Title = "title"
        };
        var result = new UpdateTagCommandValidator().Validate(command).IsValid;

        result.Should().BeTrue();
    }
    
}