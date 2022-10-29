namespace EkaterinburgDesign.Guides.Api.ApplicationOptions;

public class ApplicationOptions
{
    public bool InContainer;

    public PostgresCredentials Postgres { get; set; } = default!;
}