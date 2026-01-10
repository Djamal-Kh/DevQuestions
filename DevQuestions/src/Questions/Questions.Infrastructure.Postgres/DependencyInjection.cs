using Microsoft.Extensions.DependencyInjection;
using Questions.Application;
using Shared.Database;

namespace Questions.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgreInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddDbContext<QuestionsDbContext>();
        services.AddScoped<IQuestionsRepository, QuestionsRepository>();
        
        return services;
    }
}