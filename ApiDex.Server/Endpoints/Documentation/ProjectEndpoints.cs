using ApiDex.Application.DocumentationProjects.Commands.CreateDocumentationProject;
using ApiDex.Application.DocumentationProjects.Commands.DeleteDocumentationProject;
using ApiDex.Application.DocumentationProjects.Commands.UpdateDocumentationProject;
using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Application.DocumentationProjects.Queries.GetDocumentationProjectById;
using ApiDex.Application.DocumentationProjects.Queries.GetDocumentationProjects;
using ApiDex.Server.Results;
using MediatR;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace ApiDex.Server.Endpoints.Documentation;

public static partial class DocumentationEndpoints
{
    private static void MapProjectEndpoints(RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/CreateProjectDocumentation",
                async (
                    CreateDocumentationProjectIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = CreateDocumentationProjectRequest.FromMessage(message);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapGet(
                "/GetProjectDocumentation/{documentationProjectId:guid}",
                async (
                    Guid documentationProjectId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = GetDocumentationProjectByIdRequest.FromId(documentationProjectId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<DocumentationProjectOut>();

        group
            .MapGet(
                "/GetProjectDocumentations",
                async (IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var request = GetDocumentationProjectsRequest.Create();
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(value => HttpResults.Ok(value));
                }
            )
            .Produces<List<DocumentationProjectSummaryOut>>();

        group
            .MapPut(
                "/UpdateProjectDocumentation/{documentationProjectId:guid}",
                async (
                    Guid documentationProjectId,
                    UpdateDocumentationProjectIn message,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = UpdateDocumentationProjectRequest.FromMessage(
                        documentationProjectId,
                        message
                    );
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);

        group
            .MapDelete(
                "/DeleteProjectDocumentation/{documentationProjectId:guid}",
                async (
                    Guid documentationProjectId,
                    IMediator mediator,
                    CancellationToken cancellationToken
                ) =>
                {
                    var request = DeleteDocumentationProjectRequest.FromId(documentationProjectId);
                    var result = await mediator.Send(request, cancellationToken);

                    return result.ToMinimalResult(() => HttpResults.Ok());
                }
            )
            .Produces(StatusCodes.Status200OK);
    }
}
