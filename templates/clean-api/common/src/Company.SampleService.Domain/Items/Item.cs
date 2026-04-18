using Company.SampleService.Domain.Exceptions;

namespace Company.SampleService.Domain.Items;

public sealed class Item
{
    private Item()
    {
    }

    private Item(Guid id, string name, decimal price, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Price = price;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static Item Create(string name, decimal price)
    {
        var normalizedName = name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            throw new DomainException("Item name is required.");
        }

        if (price <= 0)
        {
            throw new DomainException("Item price must be greater than zero.");
        }

        return new Item(Guid.NewGuid(), normalizedName, price, DateTime.UtcNow);
    }
}
