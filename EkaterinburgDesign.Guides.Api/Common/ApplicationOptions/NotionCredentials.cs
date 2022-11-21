using EkaterinburgDesign.Guides.Api.Common.ApplicationOptions.EnvironmentVariables;

namespace EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;

public class NotionCredentials
{
    public string AuthToken { get; set; } = default!;

    public Guid[] Pages { get; set; } = default!;

    public static NotionCredentials CreateFromEnv() =>
        new()
        {
            AuthToken = Env.Get("NOTION_TOKEN"),
            Pages = Env
                .Get("PAGES")
                .Split(',')
                .Select(Guid.Parse)
                .ToArray()
        };
}