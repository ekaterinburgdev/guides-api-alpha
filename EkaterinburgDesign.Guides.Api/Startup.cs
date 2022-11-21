using EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;
using EkaterinburgDesign.Guides.Api.Common.Integrations.Notion;
using EkaterinburgDesign.Guides.Api.Common.Integrations.Postgres;

namespace EkaterinburgDesign.Guides.Api;

public static class Startup
{
    public static void AddLogging(ILoggingBuilder builder)
    {
        builder.ClearProviders();
        builder.AddConsole();
    }
    
    public static void ConfigureServices(IServiceCollection services) =>
        services
            .AddApplicationOptions()
            .AddPostgres()
            .AddNotion()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers();

    public static void ConfigureApplication(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}