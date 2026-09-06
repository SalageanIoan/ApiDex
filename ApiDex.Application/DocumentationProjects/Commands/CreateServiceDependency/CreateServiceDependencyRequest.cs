using ApiDex.Application.DocumentationProjects.Dtos;
using ApiDex.Domain.Results;
using MediatR;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceDependency;

public sealed record CreateServiceDependencyRequest(CreateServiceDependencyIn Message) : IRequest<Result>
{
    public static CreateServiceDependencyRequest FromMessage(CreateServiceDependencyIn message)
    {
        return new CreateServiceDependencyRequest(message);
    }
}
