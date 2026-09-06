using ApiDex.Domain.DocumentationProjects;
using ApiDex.Infrastructure.Data.Entity;

namespace ApiDex.Infrastructure.DocumentationProjects.Mappers;

public static class DocumentationProjectMappings
{
    public static DocumentationProject MapToDocumentationProject(DocumentationProjectEntity entity)
    {
        return new DocumentationProject
        {
            Id = entity.Id,
            Title = entity.Title,
            GeneralDescription = entity.GeneralDescription,
            ArchitectureType = entity.ArchitectureType,
            Services = entity.Services.Select(MapToServiceDocumentation).ToList()
        };
    }

    public static ServiceDocumentation MapToServiceDocumentation(ServiceDocumentationEntity entity)
    {
        return new ServiceDocumentation
        {
            Id = entity.Id,
            Title = entity.Title,
            GeneralDescription = entity.GeneralDescription,
            Endpoints = entity.Endpoints.Select(MapToServiceEndpoint).ToList(),
            Events = entity.Events.Select(MapToServiceEvent).ToList(),
            Dependencies = entity.Dependencies.Select(MapToServiceDependency).ToList()
        };
    }

    public static ServiceEndpoint MapToServiceEndpoint(ServiceEndpointEntity entity)
    {
        return new ServiceEndpoint
        {
            Id = entity.Id,
            ServiceDocumentationId = entity.ServiceDocumentationId,
            ServiceTitle = entity.ServiceDocumentation.Title,
            Key = entity.Key,
            HttpMethod = entity.HttpMethod,
            Route = entity.Route,
            Description = entity.Description
        };
    }

    public static ServiceEvent MapToServiceEvent(ServiceEventEntity entity)
    {
        return new ServiceEvent
        {
            Id = entity.Id,
            ServiceEndpointIds = entity.EventEndpoints
                .Select(link => link.ServiceEndpointId)
                .ToList(),
            Name = entity.Name,
            Description = entity.Description,
            Direction = entity.Direction
        };
    }

    public static ServiceDependency MapToServiceDependency(ServiceDependencyEntity entity)
    {
        return new ServiceDependency
        {
            Id = entity.Id,
            SourceEndpointId = entity.SourceEndpointId,
            DependencyType = entity.DependencyType,
            TargetEndpointId = entity.TargetEndpointId,
            TargetServiceTitle = entity.TargetServiceTitle,
            TargetEndpointRoute = entity.TargetEndpointRoute,
            Description = entity.Description
        };
    }
}
