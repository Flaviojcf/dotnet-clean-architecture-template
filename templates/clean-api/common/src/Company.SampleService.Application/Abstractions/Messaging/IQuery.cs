#if (useMediatR)
using MediatR;
using Company.SampleService.Domain;
#endif

namespace Company.SampleService.Application.Abstractions.Messaging;

#if (useMediatR)
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
#else
public interface IQuery<TResponse>
{
}
#endif
