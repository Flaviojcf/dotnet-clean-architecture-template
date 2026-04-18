using Company.SampleService.Domain.Abstractions;
using Company.SampleService.Domain.Items;
using Microsoft.EntityFrameworkCore;

namespace Company.SampleService.Infrastructure.PostgreSql.Persistence;

public sealed class CleanApiDbContext : DbContext, IUnitOfWork
{
    public CleanApiDbContext(DbContextOptions<CleanApiDbContext> options) : base(options)
    {
    }

    public DbSet<Item> Items => Set<Item>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanApiDbContext).Assembly);
    }
}
