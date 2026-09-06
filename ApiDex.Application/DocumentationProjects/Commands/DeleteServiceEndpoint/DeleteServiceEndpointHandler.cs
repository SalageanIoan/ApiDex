using ApiDex.Domain.DocumentationProjects;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.DeleteServiceEndpoint;

public class DeleteServiceEndpointHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<DeleteServiceEndpointRequest>
{
    public override async Task<Result> Handle(DeleteServiceEndpointRequest command,
        CancellationToken cancellationToken)
    {
        var deleted = await documentationProjectRepository.DeleteServiceEndpointAsync(
            command.ServiceEndpointId,
            cancellationToken);

        if (!deleted)
        {
            return Error.NotFound(
                "ServiceEndpoint.NotFound",
                $"Service endpoint '{command.ServiceEndpointId}' was not found.");
        }

        return Result.Success();
    }
}