using Company.SampleService.Domain.Items;
using MongoDB.Driver;

namespace Company.SampleService.Infrastructure.MongoDb.Persistence;

public sealed class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<Item> Items => _database.GetCollection<Item>("items");
}
