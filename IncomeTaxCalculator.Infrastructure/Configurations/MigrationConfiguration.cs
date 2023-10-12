using FluentMigrator.Runner;
using IncomeTaxCalculator.Infrastructure.Migrations;
using IncomeTaxCalculator.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeTaxCalculator.Infrastructure.Configurations;

/// <summary>
/// Configuration to handle setting up the migration scripts of the database.
/// </summary>
public static class MigrationConfiguration
{
    /// <summary>
    /// Configures the dependency injection service to include a migration script.
    /// </summary>
    /// <param name="serviceCollection">The service collection to extend.</param>
    /// <param name="dbOptions">The database options to apply.</param>
    /// <returns>An update service collection.</returns>
    public static ServiceProvider AddHrMigration(this IServiceCollection serviceCollection, DbOptions dbOptions)
    {
        return serviceCollection
            // Add common FluentMigrator services.
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add SQLite support to FluentMigrator.
                .AddSQLite()
                .ConfigureGlobalProcessorOptions(opt => { opt.ProviderSwitches = "Force Quote=false"; })
                // Set the connection string.
                .WithGlobalConnectionString(dbOptions.HrConnectionString)
                // Define the assembly containing the migrations.
                .ScanIn(typeof(HrVersionOneMigration).Assembly).For.Migrations())
            // Enable logging to console in the FluentMigrator way.
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            // Build the service provider.
            .BuildServiceProvider(false);
    }

    /// <summary>
    /// Method to update the HR database.
    /// </summary>
    /// <param name="serviceProvider">The service provider to query.</param>
    public static void UpdateHrDatabase(this IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        IMigrationRunner? runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
    }
}