using ApiDex.Application.Rag.Commands.ClearProjectIndex;
using ApiDex.Application.Rag.Commands.IndexProject;
using ApiDex.Application.Rag.Commands.UpdateRagSettings;
using ApiDex.Application.Rag.Dtos;
using ApiDex.Application.Rag.Queries.GetProjectIndexStatus;
using ApiDex.Application.Rag.Queries.GetRagSettings;
using MediatR;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace ApiDex.Server.Endpoints.Rag;

public static class RagEndpoints
{
    public static void MapRagEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/Rag")
            .WithTags("Rag");

        group.MapPost("/IndexProject/{documentationProjectId:guid}",
                async (Guid documentationProjectId, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var request = new IndexProjectCommand(documentationProjectId);
                    var result = await mediator.Send(request, cancellationToken);
                    return result ? HttpResults.Ok() : HttpResults.NotFound();
                })
            .Produces(StatusCodes.Status200OK);

        group.MapDelete("/ProjectIndex/{documentationProjectId:guid}",
                async (Guid documentationProjectId, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var request = new ClearProjectIndexCommand(documentationProjectId);
                    var result = await mediator.Send(request, cancellationToken);
                    return result ? HttpResults.Ok() : HttpResults.NotFound();
                })
            .Produces(StatusCodes.Status200OK);

        group.MapGet("/ProjectIndex/{documentationProjectId:guid}",
                async (Guid documentationProjectId, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var request = new GetProjectIndexStatusQuery(documentationProjectId);
                    var result = await mediator.Send(request, cancellationToken);
                    return HttpResults.Ok(result);
                })
            .Produces<ProjectIndexStatusOut>();

        group.MapGet("/Settings",
                async (IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var request = GetRagSettingsQuery.Create();
                    var result = await mediator.Send(request, cancellationToken);
                    return HttpResults.Ok(result);
                })
            .Produces<RagSettingsOut>();

        group.MapPut("/Settings",
                async (UpdateRagSettingsIn message, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var request = new UpdateRagSettingsCommand(message);
                    var result = await mediator.Send(request, cancellationToken);
                    return result.IsSuccess ? HttpResults.Ok() : HttpResults.BadRequest(result.Error);
                })
            .Produces(StatusCodes.Status200OK);
    }
}
