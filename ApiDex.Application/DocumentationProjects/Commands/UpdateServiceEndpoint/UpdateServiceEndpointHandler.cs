using ApiDex.Domain.DocumentationProjects;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;

namespace ApiDex.Application.DocumentationProjects.Commands.UpdateServiceEndpoint;

public class UpdateServiceEndpointHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<UpdateServiceEndpointRequest>
{
    public override async Task<Result> Handle(UpdateServiceEndpointRequest command,
        CancellationToken cancellationToken)
    {
        var message = command.Message;

        var updated = await documentationProjectRepository.UpdateServiceEndpointAsync(
            command.ServiceEndpointId,
            message.HttpMethod.Trim().ToUpperInvariant(),
            message.Route.Trim(),
            message.Description.Trim(),
            cancellationToken);

        if (!updated)
        {
            return Error.NotFound(
                "ServiceEndpoint.NotFound",
                $"Service endpoint '{command.ServiceEndpointId}' was not found.");
        }

        return Result.Success();
    }
}