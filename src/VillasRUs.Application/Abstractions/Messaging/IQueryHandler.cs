using MediatR;
using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}