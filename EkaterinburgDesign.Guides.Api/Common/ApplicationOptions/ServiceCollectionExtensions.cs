namespace EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services) =>
        services
            .AddSingleton(PostgresCredentials.CreateFromEnv())
            .AddSingleton(NotionCredentials.CreateFromEnv())
            .AddSingleton(CreateApplicationOptions);

    private static ApplicationOptions CreateApplicationOptions(IServiceProvider services) =>
        new()
        {
            Postgres = services.GetRequiredService<PostgresCredentials>()
        };
}