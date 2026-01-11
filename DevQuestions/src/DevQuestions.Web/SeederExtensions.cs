using Questions.Infrastructure.Postgres;

namespace DevQuestions.Web;

public static class SeederExtensions
{
    public static async Task<WebApplication> UseSeeders(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var seeders = scope.ServiceProvider.GetServices<ISeeder>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }

        return app;
    }
}