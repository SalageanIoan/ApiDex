using ApiDex.Domain.DocumentationProjects;
using ApiDex.Application.Mediator.Commands;
using ApiDex.Domain.Results;
using System.Text.RegularExpressions;

namespace ApiDex.Application.DocumentationProjects.Commands.CreateServiceEndpoint;

public class CreateServiceEndpointHandler(IDocumentationProjectRepository documentationProjectRepository)
    : CommandHandler<CreateServiceEndpointRequest>
{
    public override async Task<Result> Handle(CreateServiceEndpointRequest command,
        CancellationToken cancellationToken)
    {
        var message = command.Message;
        var httpMethod = message.HttpMethod.Trim().ToUpperInvariant();
        var route = message.Route.Trim();
        var key = string.IsNullOrWhiteSpace(message.Key)
            ? GenerateEndpointKey(httpMethod, route)
            : message.Key.Trim();

        await documentationProjectRepository.CreateServiceEndpointAsync(
            message.ServiceDocumentationId,
            key,
            httpMethod,
            route,
            message.Description.Trim(),
            cancellationToken);

        return Result.Success();
    }

    private static string GenerateEndpointKey(string httpMethod, string route)
    {
        var routeKey = Regex.Replace(route, "[{}]", string.Empty);
        routeKey = Regex.Replace(routeKey, "[^a-zA-Z0-9]+", "-").Trim('-').ToLowerInvariant();

        var key = $"{httpMethod.ToLowerInvariant()}-{(string.IsNullOrWhiteSpace(routeKey) ? "root" : routeKey)}";
        return key.Length <= 100 ? key : key[..100];
    }
}
