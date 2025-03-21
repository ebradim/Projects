using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CustomTicketStore.Shared.Infrastructures.Persistence;

public static class PersistenceExtension
{
    private static NpgsqlDataSourceBuilder? npgsqlDataSourceBuilder = null;
    private static NpgsqlDataSource? npgsqlDastaSource = null;
    private static readonly Lock _lock = new();

    private static NpgsqlDataSource GetNpgsqlDataSource(string connectionStr)
    {
        using (_lock.EnterScope())
        {
            if (npgsqlDastaSource is not null && npgsqlDataSourceBuilder is not null)
                return npgsqlDastaSource;

            npgsqlDataSourceBuilder = new NpgsqlDataSourceBuilder(connectionStr);
            npgsqlDataSourceBuilder.UseNodaTime();

            npgsqlDastaSource = npgsqlDataSourceBuilder.Build();

            return npgsqlDastaSource;
        }

    }
    public static void AddPersistenceModule(this IServiceCollection services, string connectionUrl)
    {

        var dataSource = GetNpgsqlDataSource(connectionUrl);

        services.AddDbContext<DataContext>((sp, builder) =>
        {

            builder.UseNpgsql(dataSource, options =>
            {
                options.EnableRetryOnFailure().UseNodaTime();
            });
            if (sp.GetRequiredService<IHostEnvironment>().IsDevelopment())
            {
                builder.EnableDetailedErrors().EnableSensitiveDataLogging();
            }


        });
    }
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();

    }
}
