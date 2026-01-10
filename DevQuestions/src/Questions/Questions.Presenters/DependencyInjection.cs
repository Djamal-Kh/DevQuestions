using Microsoft.Extensions.DependencyInjection;
using Questions.Application;

namespace Questions.Presenters;

public static class DependencyInjection
{
    public static IServiceCollection AddQuestionsModule(this IServiceCollection services)
    {
        services.AddApplication();

        // и все остальные слои Question

        return services;
    }
}