using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceDocumentation;

public sealed record UpdateServiceDocumentationRequest(
    Guid ServiceDocumentationId,
    UpdateServiceDocumentationIn Message) : IRequest<Result>
{
    public static UpdateServiceDocumentationRequest FromMessage(Guid serviceDocumentationId,
        UpdateServiceDocumentationIn message)
    {
        return new UpdateServiceDocumentationRequest(serviceDocumentationId, message);
    }
}
