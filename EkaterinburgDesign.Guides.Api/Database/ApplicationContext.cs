using EkaterinburgDesign.Guides.Api.ApplicationOptions;
using EkaterinburgDesign.Guides.Api.Database.models;
using Microsoft.EntityFrameworkCore;

namespace EkaterinburgDesign.Guides.Api.Database;

public class ApplicationContext : DbContext
{
    private readonly PostgresCredentials postgresCredentials;

    public DbSet<TestEntity> TestEntities => Set<TestEntity>();

    public ApplicationContext(PostgresCredentials postgresCredentials) =>
        this.postgresCredentials = postgresCredentials;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(postgresCredentials.ConnectionString);
}