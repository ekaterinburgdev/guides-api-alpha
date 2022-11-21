using EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;
using EkaterinburgDesign.Guides.Api.Notion;
using Notion.Client;

namespace EkaterinburgDesign.Guides.Api.Common.Integrations.Notion;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotion(this IServiceCollection services) =>
        services
            .AddSingleton<INotionClient>(x => NotionClientFactory.Create(new ClientOptions
            {
                AuthToken = x.GetRequiredService<NotionCredentials>().AuthToken
            }))
            .AddSingleton<INotionCacher, NotionCacher>();
}