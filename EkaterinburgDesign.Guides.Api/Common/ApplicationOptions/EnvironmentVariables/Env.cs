namespace EkaterinburgDesign.Guides.Api.Common.ApplicationOptions.EnvironmentVariables;

public static class Env
{
    public static string Get(string name) =>
        Environment.GetEnvironmentVariable(name) ?? throw new NullReferenceException($"No {name} environment variable");
}