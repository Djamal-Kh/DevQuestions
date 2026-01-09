using DevQuestions.Application;
using DevQuestions.Infrastructure.Postgresql;
using DevQuestions.Infrastructure.ElasticSearch;

namespace DevQuestions.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services)
    {
        services
            .AddPostgreInfrastructure()
            .AddElasticSearchInfrastructure()
            .AddWebDependencies()
            .AddApplication();

        return services;
    }

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        
        return services;
    }
}