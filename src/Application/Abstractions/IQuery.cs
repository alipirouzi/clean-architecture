using MediatR;
using SharedKernel;

namespace Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;



public interface IBaseQuery;