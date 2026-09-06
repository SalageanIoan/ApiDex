using ApiDex.Domain.Rag;

namespace ApiDex.Infrastructure.Rag.Graph.Neo4j;

public partial class Neo4JGraphStoreRepository
{
    private static string GetContextQuery(RagContextMode contextMode)
    {
        return contextMode switch
        {
            RagContextMode.Expanded => ExpandedContextQuery,
            RagContextMode.Comprehensive => ComprehensiveContextQuery,
            _ => CompactContextQuery
        };
    }

    private const string ProjectMergeQuery = """
                                             MERGE (p:Project {id: $project.id})
                                             SET p.title = $project.title,
                                                 p.description = $project.description,
                                                 p.architecture = $project.architecture
                                             """;

    private const string ServiceMergeQuery = """
                                             UNWIND $services AS service
                                             MATCH (p:Project {id: service.projectId})
                                             MERGE (s:Service {id: service.id})
                                             SET s.projectId = service.projectId,
                                                 s.title = service.title,
                                                 s.description = service.description
                                             MERGE (p)-[:HAS_SERVICE]->(s)
                                             """;

    private const string EndpointMergeQuery = """
                                              UNWIND $endpoints AS endpoint
                                              MATCH (s:Service {id: endpoint.serviceId})
                                              MERGE (e:Endpoint {id: endpoint.id})
                                              SET e.projectId = endpoint.projectId,
                                                  e.serviceId = endpoint.serviceId,
                                                  e.key = endpoint.key,
                                                  e.method = endpoint.method,
                                                  e.route = endpoint.route,
                                                  e.description = endpoint.description
                                              MERGE (s)-[:HAS_ENDPOINT]->(e)
                                              """;

    private const string EventMergeQuery = """
                                           UNWIND $events AS event
                                           MATCH (s:Service {id: event.serviceId})
                                           MERGE (ev:Event {id: event.id})
                                           SET ev.projectId = event.projectId,
                                               ev.serviceId = event.serviceId,
                                               ev.name = event.name,
                                               ev.direction = event.direction,
                                               ev.description = event.description
                                           MERGE (s)-[:HAS_EVENT]->(ev)
                                           WITH ev, event
                                           UNWIND event.endpointIds AS endpointId
                                           MATCH (e:Endpoint {id: endpointId})
                                           FOREACH (_ IN CASE WHEN event.direction = 'Published' THEN [1] ELSE [] END |
                                               MERGE (e)-[:PUBLISHES]->(ev))
                                           FOREACH (_ IN CASE WHEN event.direction = 'Consumed' THEN [1] ELSE [] END |
                                               MERGE (e)-[:CONSUMES]->(ev))
                                           """;

    private const string DependencyMergeQuery = """
                                                UNWIND $dependencies AS dependency
                                                MATCH (source:Endpoint {id: dependency.sourceEndpointId})
                                                MERGE (d:Dependency {id: dependency.id})
                                                SET d.projectId = dependency.projectId,
                                                    d.serviceId = dependency.serviceId,
                                                    d.type = dependency.type,
                                                    d.description = dependency.description,
                                                    d.targetServiceTitle = dependency.targetServiceTitle,
                                                    d.targetEndpointRoute = dependency.targetEndpointRoute
                                                MERGE (source)-[:HAS_DEPENDENCY]->(d)
                                                WITH source, d, dependency
                                                OPTIONAL MATCH (target:Endpoint {id: dependency.targetEndpointId})
                                                FOREACH (_ IN CASE WHEN target IS NULL THEN [] ELSE [1] END |
                                                    MERGE (d)-[:TARGETS_ENDPOINT]->(target)
                                                    MERGE (source)-[:CALLS]->(target))
                                                """;

    private const string DeleteProjectQuery = """
                                              MATCH (n)
                                              WHERE n.projectId = $projectId OR n.id = $projectId
                                              DETACH DELETE n
                                              """;

    private const string CompactContextQuery = """
                                                 WITH $serviceIds AS serviceIds, $endpointIds AS endpointIds
                                                 OPTIONAL MATCH (chunkService:Service)
                                                 WHERE chunkService.id IN serviceIds
                                                 WITH endpointIds, collect(DISTINCT chunkService) AS chunkServices
                                                 OPTIONAL MATCH (anchorEndpoint:Endpoint)
                                                 WHERE anchorEndpoint.id IN endpointIds
                                                 WITH chunkServices, collect(DISTINCT anchorEndpoint) AS endpoints
                                                 CALL {
                                                     WITH endpoints
                                                     UNWIND endpoints AS endpoint
                                                     OPTIONAL MATCH (endpoint)<-[:HAS_ENDPOINT]-(service:Service)
                                                     RETURN collect(DISTINCT service) AS endpointServices
                                                 }
                                                 CALL {
                                                     WITH endpoints
                                                     UNWIND endpoints AS endpoint
                                                     OPTIONAL MATCH (endpoint)<-[:HAS_ENDPOINT]-(service:Service)
                                                     RETURN collect(DISTINCT {
                                                         service: service.title,
                                                         endpoint: endpoint.method + ' ' + endpoint.route,
                                                         endpointDescription: endpoint.description
                                                     }) AS anchorEndpoints
                                                 }
                                                 CALL {
                                                     WITH endpoints
                                                     UNWIND endpoints AS endpoint
                                                     OPTIONAL MATCH (endpoint)-[:HAS_DEPENDENCY]->(dep:Dependency)
                                                     OPTIONAL MATCH (dep)-[:TARGETS_ENDPOINT]->(target:Endpoint)
                                                     WITH collect(DISTINCT CASE WHEN dep IS NULL THEN null ELSE {
                                                         from: endpoint.method + ' ' + endpoint.route,
                                                         to: coalesce(target.method + ' ' + target.route, dep.targetEndpointRoute),
                                                         targetService: dep.targetServiceTitle,
                                                         description: dep.description
                                                     } END) AS rows
                                                     RETURN [row IN rows WHERE row IS NOT NULL] AS dependencies
                                                 }
                                                 CALL {
                                                     WITH endpoints
                                                     UNWIND endpoints AS endpoint
                                                     OPTIONAL MATCH (endpoint)-[:PUBLISHES|CONSUMES]->(ev:Event)
                                                     WITH collect(DISTINCT CASE WHEN ev IS NULL THEN null ELSE {
                                                         endpoint: endpoint.method + ' ' + endpoint.route,
                                                         event: ev.name,
                                                         direction: ev.direction,
                                                         description: ev.description
                                                     } END) AS rows
                                                     RETURN [row IN rows WHERE row IS NOT NULL] AS events
                                                 }
                                                 RETURN
                                                     [service IN chunkServices + endpointServices WHERE service IS NOT NULL | {
                                                         title: service.title,
                                                         description: service.description
                                                     }] AS services,
                                                     anchorEndpoints,
                                                     dependencies,
                                                     events
                                                 """;

    private const string ExpandedContextQuery = """
                                                WITH $serviceIds AS serviceIds, $endpointIds AS endpointIds
                                                OPTIONAL MATCH (chunkService:Service)
                                                WHERE chunkService.id IN serviceIds
                                                WITH endpointIds, collect(DISTINCT chunkService) AS chunkServices
                                                OPTIONAL MATCH (anchorEndpoint:Endpoint)
                                                WHERE anchorEndpoint.id IN endpointIds
                                                WITH chunkServices, collect(DISTINCT anchorEndpoint) AS endpoints
                                                CALL {
                                                    WITH endpoints
                                                    UNWIND endpoints AS endpoint
                                                    OPTIONAL MATCH (endpoint)<-[:HAS_ENDPOINT]-(service:Service)
                                                    RETURN collect(DISTINCT service) AS endpointServices
                                                }
                                                CALL {
                                                    WITH endpoints
                                                    UNWIND endpoints AS endpoint
                                                    OPTIONAL MATCH (endpoint)<-[:HAS_ENDPOINT]-(service:Service)
                                                    RETURN collect(DISTINCT {
                                                        service: service.title,
                                                        endpoint: endpoint.method + ' ' + endpoint.route,
                                                        endpointDescription: endpoint.description
                                                    }) AS anchorEndpoints
                                                }
                                                CALL {
                                                    WITH endpoints
                                                    UNWIND endpoints AS endpoint
                                                    OPTIONAL MATCH (endpoint)-[:HAS_DEPENDENCY]->(dep:Dependency)
                                                    OPTIONAL MATCH (dep)-[:TARGETS_ENDPOINT]->(target:Endpoint)
                                                    WITH collect(DISTINCT CASE WHEN dep IS NULL THEN null ELSE {
                                                        from: endpoint.method + ' ' + endpoint.route,
                                                        to: coalesce(target.method + ' ' + target.route, dep.targetEndpointRoute),
                                                        targetService: dep.targetServiceTitle,
                                                        description: dep.description
                                                    } END) AS rows
                                                    RETURN [row IN rows WHERE row IS NOT NULL] AS dependencies
                                                }
                                                CALL {
                                                    WITH endpoints
                                                    UNWIND endpoints AS endpoint
                                                    OPTIONAL MATCH (endpoint)-[:PUBLISHES|CONSUMES]->(ev:Event)
                                                    WITH collect(DISTINCT CASE WHEN ev IS NULL THEN null ELSE {
                                                        endpoint: endpoint.method + ' ' + endpoint.route,
                                                        event: ev.name,
                                                        direction: ev.direction,
                                                        description: ev.description
                                                    } END) AS rows
                                                    RETURN [row IN rows WHERE row IS NOT NULL] AS events
                                                }
                                                CALL {
                                                    WITH endpoints
                                                    UNWIND endpoints AS endpoint
                                                    OPTIONAL MATCH (incoming:Endpoint)-[:CALLS]->(endpoint)
                                                    WITH collect(DISTINCT CASE WHEN incoming IS NULL THEN null ELSE {
                                                        from: incoming.method + ' ' + incoming.route,
                                                        to: endpoint.method + ' ' + endpoint.route
                                                    } END) AS rows
                                                    RETURN [row IN rows WHERE row IS NOT NULL] AS incomingCalls
                                                }
                                                CALL {
                                                    WITH endpoints
                                                    UNWIND endpoints AS endpoint
                                                    OPTIONAL MATCH (endpoint)-[:CALLS]->(outgoing:Endpoint)
                                                    WITH collect(DISTINCT CASE WHEN outgoing IS NULL THEN null ELSE {
                                                        from: endpoint.method + ' ' + endpoint.route,
                                                        to: outgoing.method + ' ' + outgoing.route
                                                    } END) AS rows
                                                    RETURN [row IN rows WHERE row IS NOT NULL] AS outgoingCalls
                                                }
                                                RETURN
                                                    [service IN chunkServices + endpointServices WHERE service IS NOT NULL | {
                                                        title: service.title,
                                                        description: service.description
                                                    }] AS services,
                                                    anchorEndpoints,
                                                    dependencies,
                                                    events,
                                                    incomingCalls,
                                                    outgoingCalls
                                                """;

    private const string ComprehensiveContextQuery = """
                                                 WITH $projectId AS projectId, $serviceIds AS serviceIds, $endpointIds AS endpointIds
                                                 MATCH (p:Project)
                                                 WHERE projectId IS NULL OR p.id = projectId
                                                 OPTIONAL MATCH (p)-[:HAS_SERVICE]->(projectService:Service)
                                                 OPTIONAL MATCH (projectService)-[:HAS_ENDPOINT]->(projectEndpoint:Endpoint)
                                                 WITH serviceIds, endpointIds,
                                                      collect(DISTINCT projectService) AS projectServices,
                                                      collect(DISTINCT projectEndpoint) AS projectEndpoints
                                                 OPTIONAL MATCH (chunkService:Service)
                                                 WHERE chunkService.id IN serviceIds
                                                 WITH endpointIds, projectServices, projectEndpoints, collect(DISTINCT chunkService) AS chunkServices
                                                 OPTIONAL MATCH (anchorEndpoint:Endpoint)
                                                 WHERE anchorEndpoint.id IN endpointIds
                                                 WITH projectServices, projectEndpoints, chunkServices, collect(DISTINCT anchorEndpoint) AS endpoints
                                                 CALL {
                                                     WITH endpoints
                                                     UNWIND endpoints AS endpoint
                                                     OPTIONAL MATCH (endpoint)<-[:HAS_ENDPOINT]-(service:Service)
                                                     RETURN collect(DISTINCT service) AS endpointServices
                                                 }
                                                 CALL {
                                                     WITH projectEndpoints
                                                     UNWIND projectEndpoints AS endpoint
                                                     OPTIONAL MATCH (endpoint)<-[:HAS_ENDPOINT]-(service:Service)
                                                     RETURN collect(DISTINCT CASE WHEN endpoint IS NULL THEN null ELSE {
                                                         service: service.title,
                                                         endpoint: endpoint.method + ' ' + endpoint.route,
                                                         endpointDescription: endpoint.description
                                                     } END) AS allEndpointRows
                                                 }
                                                 CALL {
                                                     WITH endpoints
                                                     UNWIND endpoints AS endpoint
                                                     OPTIONAL MATCH (endpoint)-[:HAS_DEPENDENCY]->(dep:Dependency)
                                                     OPTIONAL MATCH (dep)-[:TARGETS_ENDPOINT]->(target:Endpoint)
                                                     WITH collect(DISTINCT CASE WHEN dep IS NULL THEN null ELSE {
                                                         from: endpoint.method + ' ' + endpoint.route,
                                                         to: coalesce(target.method + ' ' + target.route, dep.targetEndpointRoute),
                                                         targetService: dep.targetServiceTitle,
                                                         description: dep.description
                                                     } END) AS rows
                                                     RETURN [row IN rows WHERE row IS NOT NULL] AS dependencies
                                                 }
                                                 CALL {
                                                     WITH endpoints
                                                     UNWIND endpoints AS endpoint
                                                     OPTIONAL MATCH (endpoint)-[:PUBLISHES|CONSUMES]->(ev:Event)
                                                     WITH collect(DISTINCT CASE WHEN ev IS NULL THEN null ELSE {
                                                         endpoint: endpoint.method + ' ' + endpoint.route,
                                                         event: ev.name,
                                                         direction: ev.direction,
                                                         description: ev.description
                                                     } END) AS rows
                                                     RETURN [row IN rows WHERE row IS NOT NULL] AS events
                                                 }
                                                 CALL {
                                                     WITH endpoints
                                                     UNWIND endpoints AS endpoint
                                                     OPTIONAL MATCH (incoming:Endpoint)-[:CALLS]->(endpoint)
                                                     WITH collect(DISTINCT CASE WHEN incoming IS NULL THEN null ELSE {
                                                         from: incoming.method + ' ' + incoming.route,
                                                         to: endpoint.method + ' ' + endpoint.route
                                                     } END) AS rows
                                                     RETURN [row IN rows WHERE row IS NOT NULL] AS incomingCalls
                                                 }
                                                 CALL {
                                                     WITH endpoints
                                                     UNWIND endpoints AS endpoint
                                                     OPTIONAL MATCH (endpoint)-[:CALLS]->(outgoing:Endpoint)
                                                     WITH collect(DISTINCT CASE WHEN outgoing IS NULL THEN null ELSE {
                                                         from: endpoint.method + ' ' + endpoint.route,
                                                         to: outgoing.method + ' ' + outgoing.route
                                                     } END) AS rows
                                                     RETURN [row IN rows WHERE row IS NOT NULL] AS outgoingCalls
                                                 }
                                                 RETURN
                                                     [service IN projectServices + chunkServices + endpointServices WHERE service IS NOT NULL | {
                                                         title: service.title,
                                                         description: service.description
                                                     }] AS services,
                                                     [row IN allEndpointRows WHERE row IS NOT NULL] AS anchorEndpoints,
                                                     dependencies,
                                                     events,
                                                     incomingCalls,
                                                     outgoingCalls
                                                 """;
}
