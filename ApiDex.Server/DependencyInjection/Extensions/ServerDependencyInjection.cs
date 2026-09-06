namespace ApiDex.Server.DependencyInjection.Extensions;

public static class ServerDependencyInjection
{
    public static void AddServer(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors();
        services.AddProblemDetails();
    }
}
