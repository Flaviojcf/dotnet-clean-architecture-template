using Company.SampleService.Application.UseCases.Items.CreateItem;
using Company.SampleService.CommomTestsUtilities.Builders.Items;
using Company.SampleService.CommomTestsUtilities.TestDoubles;
using FluentAssertions;
using Reqnroll;

namespace Company.SampleService.FunctionalTests.Steps.Items;

[Binding]
public sealed class CreateItemStepDefinitions
{
    private CreateItemRequest _request = default!;
    private CreateItemResponse _response = default!;

    [Given("que eu possuo uma requisicao valida de item")]
    public void GivenQueEuPossuoUmaRequisicaoValidaDeItem()
    {
        _request = new CreateItemRequestBuilder().Build();
    }

    [When("eu executar o caso de uso de criacao")]
    public async Task WhenEuExecutarOCasoDeUsoDeCriacao()
    {
        var sut = new CreateItemUseCase(new InMemoryItemRepository(), new FakeUnitOfWork(), new FakeMessagePublisher());
        _response = await sut.Handle(_request, CancellationToken.None);
    }

    [Then("o item deve ser criado com sucesso")]
    public void ThenOItemDeveSerCriadoComSucesso()
    {
        _response.Id.Should().NotBeEmpty();
        _response.Name.Should().Be(_request.Name);
    }
}
