using DevQuestions.Application.FullTextSearch;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Infrastructure.ElasticSearch;

public static class DependencyInjection
{
    public static IServiceCollection AddElasticSearchInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ISearchProvider, ElasticSeachProvider>();

        return services;
    }
}