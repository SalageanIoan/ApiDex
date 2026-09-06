using ApiDex.Application.DocumentationProjects.Commands.CreateServiceEndpoint;
using ApiDex.Application.DocumentationProjects.Commands.DeleteServiceEndpoint;
using ApiDex.Application.DocumentationProjects.Commands.UpdateServiceEndpoint;
using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointById;
using ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointsByProjectId;
using ApiDex.Application.DocumentationProjects.Queries.GetServiceEndpointsByServiceDocumentationId;
using ApiDex.Server.Results;
using MediatR;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace ApiDex.Server.Endpoints.Documentation;

public static partial class DocumentationEndpoints
{
    private static void MapServiceEndpointEndpoints(RouteGroupBuilder group)
    {
        group
            .MapGet(
                "/GetServiceEndpointsByServiceId/{serviceDocumentationId:guid}",
                async (
                    Guid serviceDocumentationId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetServiceEndpointsByServiceDocumentationIdRequest.FromId(
                        serviceDocumentationId
                    );
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<List<ServiceEndpointOut>>();

        group
            .MapGet(
                "/GetServiceEndpointsByProjectId/{documentationProjectId:guid}",
                async (
                    Guid documentationProjectId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetServiceEndpointsByProjectIdRequest.FromId(
                        documentationProjectId
                    );
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<List<ServiceEndpointOut>>();

        group
            .MapPost(
                "/CreateServiceEndpoint",
                async (
                    CreateServiceEndpointIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = CreateServiceEndpointRequest.FromMessage(message);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapGet(
                "/GetServiceEndpoint/{serviceEndpointId:guid}",
                async (
                    Guid serviceEndpointId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetServiceEndpointByIdRequest.FromId(serviceEndpointId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<ServiceEndpointOut>();

        group
            .MapPut(
                "/UpdateServiceEndpoint/{serviceEndpointId:guid}",
                async (
                    Guid serviceEndpointId,
                    UpdateServiceEndpointIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = UpdateServiceEndpointRequest.FromMessage(
                        serviceEndpointId,
                        message
                    );
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapDelete(
                "/DeleteServiceEndpoint/{serviceEndpointId:guid}",
                async (
                    Guid serviceEndpointId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = DeleteServiceEndpointRequest.FromId(serviceEndpointId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);
    }
}
