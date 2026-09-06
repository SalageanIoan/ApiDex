using System.Reflection;

namespace ApiDex.Infrastructure;

public static class InfrastructureAssembly
{
    public static readonly Assembly Assembly = typeof(InfrastructureAssembly).Assembly;
}