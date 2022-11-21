using EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;
using EkaterinburgDesign.Guides.Api.Database;
using EkaterinburgDesign.Guides.Api.Repositories;
using EkaterinburgDesign.Guides.Api.Repositories.Abstraction;

namespace EkaterinburgDesign.Guides.Api.Common.Integrations.Postgres;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services) =>
        services
            .AddSingleton<PostgresContextProvider>(x =>
                () => new ApplicationContext(x.GetRequiredService<PostgresCredentials>()))
            .AddSingleton<IPageElementRepository, PageElementRepository>()
            .AddSingleton<IPageTreeNodeRepository, PageTreeNodeRepository>();
}