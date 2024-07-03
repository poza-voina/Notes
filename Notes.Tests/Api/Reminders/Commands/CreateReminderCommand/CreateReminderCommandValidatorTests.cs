using FluentAssertions;
using FluentValidation;
using Moq;
using Notes.Api.Reminders.Commands;
using Xunit;

public class CreateReminderCommandValidatorTests
{
    private readonly CreateReminderCommandValidator _validator;

    public CreateReminderCommandValidatorTests()
    {
        _validator = new CreateReminderCommandValidator();
    }

    [Fact]
    public void Validate_ShouldCorrectly_WhenTitleAndTextAreProvided()
    {
        var command = new CreateReminderCommand { Title = "Test Title", Text = "Test Text", ReminderTime = DateTime.Now };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ShouldError_WhenTitleAndTextAreBothNullOrEmpty()
    {
        var command = new CreateReminderCommand { Title = null, Text = null, ReminderTime = DateTime.Now };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_ShouldError_TitleIsNullAndTextIsEmpty()
    {
        var command = new CreateReminderCommand { Title = null, Text = "", ReminderTime = DateTime.Now };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_ShouldError_TextIsNullAndTitleIsEmpty()
    {
        var command = new CreateReminderCommand { Title = "", Text = null, ReminderTime = DateTime.Now };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Validate_Error_When_Title_Is_Null_And_Text_Is_Null()
    {
        var command = new CreateReminderCommand { Title = null, Text = null, ReminderTime = DateTime.Now };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Validate_Error_When_ReminderTime_Is_Empty()
    {
        var command = new CreateReminderCommand { Title = "Test Title", Text = "Test Text" };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
    }
}
