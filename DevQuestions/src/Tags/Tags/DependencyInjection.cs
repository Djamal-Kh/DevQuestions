using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;
using Tags.Contracts;
using Tags.Database;
using Tags.Feature;

namespace Tags;

public static class DependencyInjection
{
    public static IServiceCollection AddTagsModule(this IServiceCollection services)
    {
        services.AddDbContext<TagsDbContext>();

        var assembly = typeof(DependencyInjection).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.AddScoped<ITagsContract, TagsContracts>();

        return services;
    }
}