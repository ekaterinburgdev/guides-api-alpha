namespace EkaterinburgDesign.Guides.Api.ApplicationOptions.EnvironmentVariables;

public static class EnvironmentVariablesConfigurator
{
    public static void LoadVariables()
    {
        var inContainerParsed = bool.TryParse(Environment.GetEnvironmentVariable("IN_CONTAINER"), out var inContainer);

        if (inContainerParsed && inContainer)
        {
            return;
        }

        var root = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var filePath = Path.Combine(root, ".env");

        if (!File.Exists(filePath))
            throw new FileNotFoundException(
                "Environment configuration file must exist if the application is running outside of docker container. " +
                "If you see this exception while running inside docker container, then specify container " +
                "environment variable \"IN_CONTAINER\" and set It's value to true");

        File.ReadAllLines(filePath).ToList().ForEach(AddVariable);
    }

    private static void AddVariable(string envLine)
    {
        var parts = envLine.Split(
            '=',
            StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
            return;

        Environment.SetEnvironmentVariable(parts[0], parts[1]);
    }
}