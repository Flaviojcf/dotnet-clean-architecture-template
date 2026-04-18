using Company.SampleService.Application.UseCases.Items.GetItemById;
using Company.SampleService.CommomTestsUtilities.Builders.Items;
using Company.SampleService.CommomTestsUtilities.TestDoubles;
using Company.SampleService.Domain.Exceptions;
using FluentAssertions;

namespace Company.SampleService.UnitTests.Application.UseCases.Items.GetItemById;

public sealed class GetItemByIdUseCaseTests
{
    [Fact]
    public async Task Given_ExistingItem_When_Handle_Then_ShouldReturnItem()
    {
        var repository = new InMemoryItemRepository();
        var item = new ItemBuilder().Build();
        await repository.AddAsync(item, CancellationToken.None);
        var sut = new GetItemByIdUseCase(repository);

        var response = await sut.Handle(new GetItemByIdRequest(item.Id), CancellationToken.None);

        response.Id.Should().Be(item.Id);
        response.Name.Should().Be(item.Name);
    }

    [Fact]
    public async Task Given_UnknownItem_When_Handle_Then_ShouldThrowNotFoundException()
    {
        var sut = new GetItemByIdUseCase(new InMemoryItemRepository());

        var action = async () => await sut.Handle(new GetItemByIdRequest(Guid.NewGuid()), CancellationToken.None);

        await action.Should().ThrowAsync<NotFoundException>();
    }
}
