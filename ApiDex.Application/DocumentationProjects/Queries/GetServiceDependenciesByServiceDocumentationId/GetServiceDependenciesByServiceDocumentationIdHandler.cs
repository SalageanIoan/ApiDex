using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.DocumentationProjects;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Queries.GetServiceDependenciesByServiceDocumentationId;

public class GetServiceDependenciesByServiceDocumentationIdHandler
    (IDocumentationProjectRepository documentationProjectRepository)
    : IRequestHandler<GetServiceDependenciesByServiceDocumentationIdRequest,
        Result<List<ServiceDependencyOut>>>
{
    public async Task<Result<List<ServiceDependencyOut>>> Handle(
        GetServiceDependenciesByServiceDocumentationIdRequest request,
        CancellationToken cancellationToken)
    {
        var dependencies = await documentationProjectRepository
            .GetServiceDependenciesByServiceDocumentationIdAsync(
                request.ServiceDocumentationId,
                cancellationToken);

        var result = dependencies.Select(dependency => new ServiceDependencyOut
        {
            Id = dependency.Id,
            SourceEndpointId = dependency.SourceEndpointId,
            DependencyType = dependency.DependencyType,
            TargetEndpointId = dependency.TargetEndpointId,
            TargetServiceTitle = dependency.TargetServiceTitle,
            TargetEndpointRoute = dependency.TargetEndpointRoute,
            Description = dependency.Description
        }).ToList();

        return result;
    }
}