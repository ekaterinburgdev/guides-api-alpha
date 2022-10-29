using EkaterinburgDesign.Guides.Api.ApplicationOptions.EnvironmentVariables;

namespace EkaterinburgDesign.Guides.Api.ApplicationOptions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services) =>
        services
            .AddSingleton(CreatePostgresCredentials())
            .AddSingleton(x => new ApplicationOptions
            {
                Postgres = x.GetRequiredService<PostgresCredentials>()
            });

    private static PostgresCredentials CreatePostgresCredentials() =>
        new()
        {
            Host = EnvironmentVariablesProvider.GetRequiredVariable("DB_HOST"),
            Port = EnvironmentVariablesProvider.GetRequiredVariable("DB_PORT"),
            Login = EnvironmentVariablesProvider.GetRequiredVariable("DB_USER"),
            DatabaseName = EnvironmentVariablesProvider.GetRequiredVariable("DB_NAME"),
            Password = EnvironmentVariablesProvider.GetRequiredVariable("DB_PASSWORD"),
        };
}