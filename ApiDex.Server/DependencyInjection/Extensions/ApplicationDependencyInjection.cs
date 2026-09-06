using ApiDex.Application;
using FluentValidation;
using ApiDex.Application.Mediator.Behaviours;
using MediatR;

namespace ApiDex.Server.DependencyInjection.Extensions;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(ApplicationAssembly.Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(ApplicationAssembly.Assembly);
    }
}
