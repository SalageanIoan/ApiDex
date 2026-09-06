using ApiDex.Application.DocumentationProjects.Commands.CreateServiceEvent;
using ApiDex.Application.DocumentationProjects.Commands.DeleteServiceEvent;
using ApiDex.Application.DocumentationProjects.Commands.UpdateServiceEvent;
using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Application.DocumentationProjects.Queries.GetServiceEventById;
using ApiDex.Application.DocumentationProjects.Queries.GetServiceEventsByServiceDocumentationId;
using ApiDex.Server.Results;
using MediatR;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace ApiDex.Server.Endpoints.Documentation;

public static partial class DocumentationEndpoints
{
    private static void MapServiceEventEndpoints(RouteGroupBuilder group)
    {
        group
            .MapGet(
                "/GetServiceEventsByServiceId/{serviceDocumentationId:guid}",
                async (
                    Guid serviceDocumentationId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetServiceEventsByServiceDocumentationIdRequest.FromId(
                        serviceDocumentationId
                    );
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<List<ServiceEventOut>>();

        group
            .MapPost(
                "/CreateServiceEvent",
                async (
                    CreateServiceEventIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = CreateServiceEventRequest.FromMessage(message);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapGet(
                "/GetServiceEvent/{serviceEventId:guid}",
                async (
                    Guid serviceEventId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetServiceEventByIdRequest.FromId(serviceEventId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<ServiceEventOut>();

        group
            .MapPut(
                "/UpdateServiceEvent/{serviceEventId:guid}",
                async (
                    Guid serviceEventId,
                    UpdateServiceEventIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = UpdateServiceEventRequest.FromMessage(serviceEventId, message);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapDelete(
                "/DeleteServiceEvent/{serviceEventId:guid}",
                async (
                    Guid serviceEventId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = DeleteServiceEventRequest.FromId(serviceEventId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);
    }
}
