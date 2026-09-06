using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceDocumentation;

public sealed record DeleteServiceDocumentationRequest(Guid ServiceDocumentationId) : IRequest<Result>
{
    public static DeleteServiceDocumentationRequest FromId(Guid serviceDocumentationId)
    {
        return new DeleteServiceDocumentationRequest(serviceDocumentationId);
    }
}
