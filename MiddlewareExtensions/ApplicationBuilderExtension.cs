using RealTimeDatabase.SubscribeTableDependencies;

namespace RealTimeDatabase.MiddlewareExtensions;

public static class ApplicationBuilderExtension
{
    public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder, string connectionString)
    where T : ISubscribeTableDependencies
    {
        var serviceProvider = applicationBuilder.ApplicationServices;
        var service = serviceProvider.GetService<T>();
        service!.SubscribeTableDependency(connectionString);
    }
}