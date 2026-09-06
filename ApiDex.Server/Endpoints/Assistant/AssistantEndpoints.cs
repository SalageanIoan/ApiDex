using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Application.Rag.Queries.AskDocumentation;
using MediatR;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace ApiDex.Server.Endpoints.Assistant;

public static class AssistantEndpoints
{
    public static void MapAssistantEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/Assistant")
            .WithTags("Assistant");

        group.MapPost("/Ask",
                async (AskDocumentationIn message, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var request = new AskDocumentationQuery(message.Question, message.ProjectId, message.ServiceId);
                    var result = await mediator.Send(request, cancellationToken);
                    return HttpResults.Ok(new { answer = result.Answer, usage = result.Usage });
                })
            .Produces(StatusCodes.Status200OK);
    }
}
