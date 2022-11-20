using EkaterinburgDesign.Guides.Api.ApplicationOptions;
using EkaterinburgDesign.Guides.Api.Integrations.Postgres;

namespace EkaterinburgDesign.Guides.Api;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services) =>
        services
            .AddApplicationOptions()
            .AddPostgres()
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