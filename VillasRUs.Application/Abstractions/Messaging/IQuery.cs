using MediatR;
using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}