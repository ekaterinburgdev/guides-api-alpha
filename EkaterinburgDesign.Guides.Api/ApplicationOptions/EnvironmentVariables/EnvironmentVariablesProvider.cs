namespace EkaterinburgDesign.Guides.Api.ApplicationOptions.EnvironmentVariables;

public static class EnvironmentVariablesProvider
{
    public static string GetRequiredVariable(string name) =>
        Environment.GetEnvironmentVariable(name) ?? throw new NullReferenceException($"No {name} environment variable");
}