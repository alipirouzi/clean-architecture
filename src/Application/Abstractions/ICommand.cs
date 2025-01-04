using MediatR;
using SharedKernel;

namespace Application.Abstractions;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;