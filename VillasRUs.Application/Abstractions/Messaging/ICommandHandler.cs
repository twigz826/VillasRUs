using MediatR;
using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
where TCommand : ICommand<TResponse>
{
}