namespace EkaterinburgDesign.Guides.Api.ApplicationOptions;

public class PostgresCredentials
{
    public string Host { get; set; } = default!;

    public string Port { get; set; } = default!;

    public string DatabaseName { get; set; } = default!;

    public string Login { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string ConnectionString =>
        $"Host={Host};Port={Port};Username={Login};Password={Password};Database={DatabaseName}";
}