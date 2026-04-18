using Company.SampleService.Application.UseCases.Items.CreateItem;
using Company.SampleService.CommomTestsUtilities.Builders.Items;
using Company.SampleService.CommomTestsUtilities.TestDoubles;
using Company.SampleService.Domain.Exceptions;
using FluentAssertions;

namespace Company.SampleService.UnitTests.Application.UseCases.Items.CreateItem;

public sealed class CreateItemUseCaseTests
{
    [Fact]
    public async Task Given_ValidRequest_When_Handle_Then_ShouldCreateItem()
    {
        var repository = new InMemoryItemRepository();
        var unitOfWork = new FakeUnitOfWork();
        var publisher = new FakeMessagePublisher();
        var request = new CreateItemRequestBuilder().Build();
        var sut = new CreateItemUseCase(repository, unitOfWork, publisher);

        var response = await sut.Handle(request, CancellationToken.None);

        response.Id.Should().NotBeEmpty();
        response.Name.Should().Be(request.Name);
        publisher.PublishedMessages.Should().ContainSingle();
        unitOfWork.SaveChangesCalls.Should().Be(1);
    }

    [Fact]
    public async Task Given_DuplicatedName_When_Handle_Then_ShouldThrowConflictException()
    {
        var repository = new InMemoryItemRepository();
        var unitOfWork = new FakeUnitOfWork();
        var publisher = new FakeMessagePublisher();
        var request = new CreateItemRequestBuilder().Build();
        var sut = new CreateItemUseCase(repository, unitOfWork, publisher);

        await repository.AddAsync(Company.SampleService.Domain.Items.Item.Create(request.Name, request.Price), CancellationToken.None);

        var action = async () => await sut.Handle(request, CancellationToken.None);

        await action.Should().ThrowAsync<ConflictException>();
    }
}
