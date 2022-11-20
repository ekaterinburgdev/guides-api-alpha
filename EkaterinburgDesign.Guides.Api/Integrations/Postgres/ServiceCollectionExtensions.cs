using EkaterinburgDesign.Guides.Api.ApplicationOptions;
using EkaterinburgDesign.Guides.Api.Database;

namespace EkaterinburgDesign.Guides.Api.Integrations.Postgres;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services) =>
        services
            .AddSingleton<PostgresContextProvider>(x =>
                () => new ApplicationContext(x.GetRequiredService<PostgresCredentials>()));
}