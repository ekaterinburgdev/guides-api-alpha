namespace EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;

public class ApplicationOptions
{
    public PostgresCredentials Postgres { get; set; } = default!;

    public NotionCredentials Notion { get; set; } = default!;
}