using EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;
using EkaterinburgDesign.Guides.Api.Database.models;
using Microsoft.EntityFrameworkCore;

namespace EkaterinburgDesign.Guides.Api.Database;

public class ApplicationContext : DbContext
{
    private readonly PostgresCredentials postgresCredentials;

    public DbSet<PageElement> PageElements => Set<PageElement>();

    public DbSet<PageTreeNode> PageTreeNodes => Set<PageTreeNode>();

    public ApplicationContext(PostgresCredentials postgresCredentials) =>
        this.postgresCredentials = postgresCredentials;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(postgresCredentials.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PageTreeNode>()
            .HasMany(x => x.ChildNodes)
            .WithOne(x => x.ParentNode);

        modelBuilder.Entity<PageElement>()
            .HasMany(x => x.ChildPageElements)
            .WithOne(x => x.ParentPageElement);
    }
}