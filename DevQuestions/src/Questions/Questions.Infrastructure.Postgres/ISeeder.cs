namespace Questions.Infrastructure.Postgres;

public interface ISeeder
{
    Task SeedAsync();
}