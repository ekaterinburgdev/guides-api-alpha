using EkaterinburgDesign.Guides.Api;
using EkaterinburgDesign.Guides.Api.Common.ApplicationOptions.EnvironmentVariables;

EnvironmentVariablesConfigurator.LoadVariables();

var builder = WebApplication.CreateBuilder(args);

Startup.ConfigureServices(builder.Services);
Startup.AddLogging(builder.Logging);

var app = builder.Build();
Startup.ConfigureApplication(app);

app.Run();