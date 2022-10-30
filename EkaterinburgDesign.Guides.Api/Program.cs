using EkaterinburgDesign.Guides.Api;
using EkaterinburgDesign.Guides.Api.ApplicationOptions.EnvironmentVariables;

EnvironmentVariablesConfigurator.LoadVariables();

var builder = WebApplication.CreateBuilder(args);

Configuration.ConfigureServices(builder.Services);

var app = builder.Build();
Configuration.ConfigureApplication(app);

app.Run();