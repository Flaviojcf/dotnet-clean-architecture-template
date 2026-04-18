using Bogus;
using Company.SampleService.Domain.Items;

namespace Company.SampleService.CommomTestsUtilities.Builders.Items;

public sealed class ItemBuilder
{
    private readonly Faker _faker = new();

    public Item Build(string? name = null, decimal? price = null)
    {
        return Item.Create(
            name ?? _faker.Commerce.ProductName(),
            price ?? decimal.Parse(_faker.Commerce.Price(10, 500)));
    }
}
