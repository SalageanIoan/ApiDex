using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.Mediator.Commands;

public interface ICommand : IRequest<Result>;
