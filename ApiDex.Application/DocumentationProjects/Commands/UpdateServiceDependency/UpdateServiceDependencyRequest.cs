using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceDependency;

public sealed record UpdateServiceDependencyRequest(
    Guid ServiceDependencyId,
    UpdateServiceDependencyIn Message) : IRequest<Result>
{
    public static UpdateServiceDependencyRequest FromMessage(Guid serviceDependencyId,
        UpdateServiceDependencyIn message)
    {
        return new UpdateServiceDependencyRequest(serviceDependencyId, message);
    }
}
