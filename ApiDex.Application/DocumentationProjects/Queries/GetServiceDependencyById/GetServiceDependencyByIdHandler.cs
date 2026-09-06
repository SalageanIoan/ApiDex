using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDependencyById;

public class GetServiceDependencyByIdHandler(IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetServiceDependencyByIdRequest, Result<ServiceDependencyOut>>
{
    public async Task<Result<ServiceDependencyOut>> Handle(GetServiceDependencyByIdRequest request,
        CancellationToken cancellationToken)
    {
        var dependency = await documentationProjectRepository.GetServiceDependencyByIdAsync(
            request.ServiceDependencyId,
            cancellationToken);

        if (dependency is null)
        {
            return Error.NotFound(
                "ServiceDependency.NotFound",
                $"Service dependency '{request.ServiceDependencyId}' was not found.");
        }

        return new ServiceDependencyOut
        {
            Id = dependency.Id,
            SourceEndpointId = dependency.SourceEndpointId,
            DependencyType = dependency.DependencyType,
            TargetEndpointId = dependency.TargetEndpointId,
            TargetServiceTitle = dependency.TargetServiceTitle,
            TargetEndpointRoute = dependency.TargetEndpointRoute,
            Description = dependency.Description
        };
    }
}