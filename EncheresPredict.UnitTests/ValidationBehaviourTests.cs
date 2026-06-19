using EncheresPredict.Application.Common.Behaviours;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace EncheresPredict.UnitTests;

public class ValidationBehaviourTests
{
    private record TestCommand(string Name);

    private class TestValidator : AbstractValidator<TestCommand>
    {
        public TestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }

    [Fact]
    public async Task Should_Throw_ValidationException_When_Request_Is_Invalid()
    {
        // Arrange

        var validators = new List<IValidator<TestCommand>>
        {
            new TestValidator()
        };

        var behaviour =
            new ValidationBehaviour<TestCommand, Unit>(validators);

        var command = new TestCommand("");

        // Act

        Func<Task> action = async () =>
            await behaviour.Handle(
                command,
                () => Task.FromResult(Unit.Value),
                CancellationToken.None);

        // Assert

        await action
            .Should()
            .ThrowAsync<ValidationException>();
    }
}