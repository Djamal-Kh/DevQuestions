using Microsoft.Extensions.DependencyInjection;
using Questions.Application.FullTextSearch;

namespace Infrastructure.ElasticSearch;

public static class DependencyInjection
{
    public static IServiceCollection AddElasticSearchInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ISearchProvider, ElasticSeachProvider>();

        return services;
    }
}