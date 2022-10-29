﻿namespace EkaterinburgDesign.Guides.Api.ApplicationOptions.EnvironmentVariables;

public static class EnvironmentVariablesConfigurator
{
    public static void LoadVariables()
    {
        var root = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var filePath = Path.Combine(root, ".env");

        if (!File.Exists(filePath))
            return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(
                '=',
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue;

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}