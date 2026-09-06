using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDependencyById;

public sealed record GetServiceDependencyByIdRequest(Guid ServiceDependencyId)
    : IRequest<Result<ServiceDependencyOut>>
{
    public static GetServiceDependencyByIdRequest FromId(Guid serviceDependencyId)
    {
        return new GetServiceDependencyByIdRequest(serviceDependencyId);
    }
}
