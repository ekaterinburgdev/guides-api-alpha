using EkaterinburgDesign.Guides.Api.Common.ApplicationOptions.EnvironmentVariables;

namespace EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;

public class PostgresCredentials
{
    public string Host { get; set; } = default!;

    public string Port { get; set; } = default!;

    public string DatabaseName { get; set; } = default!;

    public string Login { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string ConnectionString =>
        $"Host={Host};Port={Port};Username={Login};Password={Password};Database={DatabaseName};IncludeErrorDetail=true";

    public static PostgresCredentials CreateFromEnv() =>
        new()
        {
            Host = Env.Get("DB_HOST"),
            Port = Env.Get("DB_PORT"),
            Login = Env.Get("DB_USER"),
            DatabaseName = Env.Get("DB_NAME"),
            Password = Env.Get("DB_PASSWORD"),
        };
}