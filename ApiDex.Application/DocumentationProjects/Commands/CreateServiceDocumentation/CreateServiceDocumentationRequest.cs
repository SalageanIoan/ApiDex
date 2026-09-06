using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceDocumentation;

public sealed record CreateServiceDocumentationRequest(CreateServiceDocumentationIn Message) : IRequest<Result>
{
    public static CreateServiceDocumentationRequest FromMessage(CreateServiceDocumentationIn message)
    {
        return new CreateServiceDocumentationRequest(message);
    }
}
