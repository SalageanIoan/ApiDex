namespace ApiDex.Server.Endpoints.Documentation;

public static partial class DocumentationEndpoints
{
    public static void MapDocumentationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/Documentation")
            .WithTags("Documentation");

        MapProjectEndpoints(group);
        MapServiceEndpoints(group);
        MapServiceEndpointEndpoints(group);
        MapServiceEventEndpoints(group);
        MapServiceDependencyEndpoints(group);
    }
}