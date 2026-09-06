using ApiDex.Application.DocumentationProjects.Commands.CreateServiceDependency;
using ApiDex.Application.DocumentationProjects.Commands.DeleteServiceDependency;
using ApiDex.Application.DocumentationProjects.Commands.UpdateServiceDependency;
using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Application.DocumentationProjects.Queries.GetServiceDependenciesByServiceDocumentationId;
using ApiDex.Application.DocumentationProjects.Queries.GetServiceDependencyById;
using ApiDex.Server.Results;
using MediatR;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace ApiDex.Server.Endpoints.Documentation;

public static partial class DocumentationEndpoints
{
    private static void MapServiceDependencyEndpoints(RouteGroupBuilder group)
    {
        group
            .MapGet(
                "/GetServiceDependenciesByServiceId/{serviceDocumentationId:guid}",
                async (
                    Guid serviceDocumentationId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetServiceDependenciesByServiceDocumentationIdRequest.FromId(
                        serviceDocumentationId
                    );
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<List<ServiceDependencyOut>>();

        group
            .MapPost(
                "/CreateServiceDependency",
                async (
                    CreateServiceDependencyIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = CreateServiceDependencyRequest.FromMessage(message);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapGet(
                "/GetServiceDependency/{serviceDependencyId:guid}",
                async (
                    Guid serviceDependencyId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetServiceDependencyByIdRequest.FromId(serviceDependencyId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<ServiceDependencyOut>();

        group
            .MapPut(
                "/UpdateServiceDependency/{serviceDependencyId:guid}",
                async (
                    Guid serviceDependencyId,
                    UpdateServiceDependencyIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = UpdateServiceDependencyRequest.FromMessage(
                        serviceDependencyId,
                        message
                    );
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapDelete(
                "/DeleteServiceDependency/{serviceDependencyId:guid}",
                async (
                    Guid serviceDependencyId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = DeleteServiceDependencyRequest.FromId(serviceDependencyId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);
    }
}
