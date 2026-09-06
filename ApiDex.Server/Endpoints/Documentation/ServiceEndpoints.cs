using ApiDex.Application.DocumentationProjects.Commands.CreateServiceDocumentation;
using ApiDex.Application.DocumentationProjects.Commands.DeleteServiceDocumentation;
using ApiDex.Application.DocumentationProjects.Commands.UpdateServiceDocumentation;
using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Application.DocumentationProjects.Queries.GetServiceDocumentationById;
using ApiDex.Application.DocumentationProjects.Queries.GetServiceDocumentationsByProjectId;
using ApiDex.Server.Results;
using MediatR;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace ApiDex.Server.Endpoints.Documentation;

public static partial class DocumentationEndpoints
{
    private static void MapServiceEndpoints(RouteGroupBuilder group)
    {
        group
            .MapGet(
                "/GetServiceDocumentationsByProjectId/{documentationProjectId:guid}",
                async (
                    Guid documentationProjectId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetServiceDocumentationsByProjectIdRequest.FromId(
                        documentationProjectId
                    );
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<List<ServiceDocumentationOut>>();

        group
            .MapPost(
                "/CreateServiceDocumentation",
                async (
                    CreateServiceDocumentationIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = CreateServiceDocumentationRequest.FromMessage(message);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapGet(
                "/GetServiceDocumentation/{serviceDocumentationId:guid}",
                async (
                    Guid serviceDocumentationId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetServiceDocumentationByIdRequest.FromId(serviceDocumentationId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<ServiceDocumentationOut>();

        group
            .MapPut(
                "/UpdateServiceDocumentation/{serviceDocumentationId:guid}",
                async (
                    Guid serviceDocumentationId,
                    UpdateServiceDocumentationIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = UpdateServiceDocumentationRequest.FromMessage(
                        serviceDocumentationId,
                        message
                    );
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapDelete(
                "/DeleteServiceDocumentation/{serviceDocumentationId:guid}",
                async (
                    Guid serviceDocumentationId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = DeleteServiceDocumentationRequest.FromId(serviceDocumentationId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);
    }
}
