using Company.SampleService.Domain;
#if (useMediatR)
using MediatR;
#endif

namespace Company.SampleService.Application.Abstractions.Messaging;

#if (useMediatR)
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
#else
public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
#endif
