using FluentMigrator.Runner;
using IncomeTaxCalculator.Infrastructure.Migrations;
using IncomeTaxCalculator.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeTaxCalculator.Infrastructure.Configurations;

public static class MigrationConfiguration
{
    /// <summary>
    /// Configure the dependency injection services
    /// </summary>
    public static ServiceProvider AddHrMigration(this IServiceCollection serviceCollection, DbOptions dbOptions)
    {
        return serviceCollection
            // Add common FluentMigrator services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add SQLite support to FluentMigrator
                .AddSQLite()
                .ConfigureGlobalProcessorOptions(opt => { opt.ProviderSwitches = "Force Quote=false"; })
                // Set the connection string
                .WithGlobalConnectionString(dbOptions.HrConnectionString)
                // Define the assembly containing the migrations
                .ScanIn(typeof(HrVersionOneMigration).Assembly).For.Migrations())
            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            // Build the service provider
            .BuildServiceProvider(false);
    }

    /// <summary>
    /// Update the database
    /// </summary>
    public static void UpdateRbacDatabase(this IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
    }
}