using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Infrastructure.Postgresql.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Infrastructure.Postgresql;

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