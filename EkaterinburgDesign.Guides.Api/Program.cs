using EkaterinburgDesign.Guides.Api;
using EkaterinburgDesign.Guides.Api.ApplicationOptions.EnvironmentVariables;

EnvironmentVariablesConfigurator.LoadVariables();

var builder = WebApplication.CreateBuilder(args);

Startup.ConfigureServices(builder.Services);

var app = builder.Build();
Startup.ConfigureApplication(app);

app.Run();