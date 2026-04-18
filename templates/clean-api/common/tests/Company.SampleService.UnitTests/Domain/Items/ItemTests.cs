using Company.SampleService.Domain.Exceptions;
using Company.SampleService.Domain.Items;
using FluentAssertions;

namespace Company.SampleService.UnitTests.Domain.Items;

public sealed class ItemTests
{
    [Fact]
    public void Given_InvalidName_When_Create_Then_ShouldThrowDomainException()
    {
        var action = () => Item.Create(string.Empty, 10);

        action.Should().Throw<DomainException>();
    }

    [Fact]
    public void Given_InvalidPrice_When_Create_Then_ShouldThrowDomainException()
    {
        var action = () => Item.Create("Notebook", 0);

        action.Should().Throw<DomainException>();
    }
}
