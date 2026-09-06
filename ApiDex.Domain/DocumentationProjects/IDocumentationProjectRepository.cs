using ApiDex.Domain.DocumentationProjects.Enums;

namespace ApiDex.Domain.DocumentationProjects;


public interface IDocumentationProjectRepository
{
    Task<Guid> CreateDocumentationProjectAsync(string title, string generalDescription,
        ESystemArchitecture architectureType,
        CancellationToken cancellationToken = default);

    Task<DocumentationProject?> GetDocumentationProjectByIdAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default);

    Task<List<DocumentationProject>> GetDocumentationProjectsAsync(
        CancellationToken cancellationToken = default);

    Task<bool> UpdateDocumentationProjectAsync(Guid documentationProjectId, string title, string generalDescription,
        ESystemArchitecture architectureType, CancellationToken cancellationToken = default);

    Task<bool> DeleteDocumentationProjectAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default);

    Task<ESystemArchitecture?> GetArchitectureTypeAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default);

    Task<ESystemArchitecture?> GetArchitectureTypeByServiceIdAsync(Guid serviceDocumentationId,
        CancellationToken cancellationToken = default);

    Task CreateServiceDocumentationAsync(Guid documentationProjectId, string title,
        string generalDescription,
        CancellationToken cancellationToken = default);

    Task<ServiceDocumentation?> GetServiceDocumentationByIdAsync(Guid serviceDocumentationId,
        CancellationToken cancellationToken = default);

    Task<List<ServiceDocumentation>> GetServiceDocumentationsByProjectIdAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateServiceDocumentationAsync(Guid serviceDocumentationId, string title,
        string generalDescription, CancellationToken cancellationToken = default);

    Task<bool> DeleteServiceDocumentationAsync(Guid serviceDocumentationId,
        CancellationToken cancellationToken = default);

    Task CreateServiceEndpointAsync(Guid serviceDocumentationId, string key,
        string httpMethod, string route, string description,
        CancellationToken cancellationToken = default);

    Task<ServiceEndpoint?> GetServiceEndpointByIdAsync(Guid serviceEndpointId,
        CancellationToken cancellationToken = default);

    Task<List<ServiceEndpoint>> GetServiceEndpointsByServiceDocumentationIdAsync(Guid serviceDocumentationId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateServiceEndpointAsync(Guid serviceEndpointId,
        string httpMethod, string route, string description,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteServiceEndpointAsync(Guid serviceEndpointId,
        CancellationToken cancellationToken = default);

    Task CreateServiceEventAsync(Guid serviceDocumentationId, IReadOnlyList<Guid> serviceEndpointIds,
        string name, string description, EServiceEventDirection direction,
        CancellationToken cancellationToken = default);

    Task<ServiceEvent?> GetServiceEventByIdAsync(Guid serviceEventId,
        CancellationToken cancellationToken = default);

    Task<List<ServiceEvent>> GetServiceEventsByServiceDocumentationIdAsync(Guid serviceDocumentationId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateServiceEventAsync(Guid serviceEventId, IReadOnlyList<Guid> serviceEndpointIds,
        string name, string description,
        EServiceEventDirection direction, CancellationToken cancellationToken = default);

    Task<bool> DeleteServiceEventAsync(Guid serviceEventId,
        CancellationToken cancellationToken = default);

    Task CreateServiceDependencyAsync(Guid serviceDocumentationId,
        Guid sourceEndpointId, EServiceDependencyType dependencyType,
        Guid? targetEndpointId, string? targetServiceTitle, string? targetEndpointRoute,
        string description,
        CancellationToken cancellationToken = default);

    Task<ServiceDependency?> GetServiceDependencyByIdAsync(Guid serviceDependencyId,
        CancellationToken cancellationToken = default);

    Task<List<ServiceDependency>> GetServiceDependenciesByServiceDocumentationIdAsync(
        Guid serviceDocumentationId, CancellationToken cancellationToken = default);

    Task<bool> UpdateServiceDependencyAsync(Guid serviceDependencyId, Guid sourceEndpointId,
        EServiceDependencyType dependencyType, Guid? targetEndpointId, string? targetServiceTitle,
        string? targetEndpointRoute, string description, CancellationToken cancellationToken = default);

    Task<bool> DeleteServiceDependencyAsync(Guid serviceDependencyId,
        CancellationToken cancellationToken = default);

    Task<List<ServiceEndpoint>> GetServiceEndpointsByProjectIdAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default);

    Task<DocumentationProject?> GetProjectDataForIndexingAsync(Guid documentationProjectId,
        CancellationToken cancellationToken = default);
}
