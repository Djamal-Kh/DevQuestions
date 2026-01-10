using Infrastructure.ElasticSearch;
using Questions.Infrastructure.Postgres;
using Questions.Presenters;
using Shared;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services)
    {
        services
            .AddPostgreInfrastructure()
            .AddElasticSearchInfrastructure()
            .AddWebDependencies()
            .AddSharedDependencies()
            .AddQuestionsModule();

        return services;
    }

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        return services;
    }
}