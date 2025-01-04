using Application.Orders.Queries;
using MediatR;
using SharedKernel;

namespace Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;