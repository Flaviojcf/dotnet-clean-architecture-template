using Company.SampleService.Domain.Abstractions;

namespace Company.SampleService.Infrastructure.MongoDb.Persistence;

public sealed class MongoUnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(1);
    }
}
