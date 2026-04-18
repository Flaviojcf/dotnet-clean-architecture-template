#if (useMediatR)
using MediatR;
using Company.SampleService.Domain;
#endif

namespace Company.SampleService.Application.Abstractions.Messaging;

#if (useMediatR)
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
#else
public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}
#endif
