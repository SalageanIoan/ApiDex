using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceDependency;

public sealed record DeleteServiceDependencyRequest(Guid ServiceDependencyId) : IRequest<Result>
{
    public static DeleteServiceDependencyRequest FromId(Guid serviceDependencyId)
    {
        return new DeleteServiceDependencyRequest(serviceDependencyId);
    }
}
