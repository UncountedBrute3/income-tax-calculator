using System.Data.Common;
using FluentMigrator;
using FluentMigrator.Runner;
using IncomeTaxCalculator.Infrastructure.Configurations;
using IncomeTaxCalculator.Infrastructure.Options;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;

namespace IncomeTaxCalculator.Presentation.Api.Tests.Integration.Configuration;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public readonly string TestDbConnection = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveServiceDescriptor<DbOptions>(services);
            RemoveServiceDescriptor<DbConnection>(services);
            RemoveServiceDescriptor<IMigrationRunner>(services);
            RemoveServiceDescriptor<IMigrationProcessor>(services);

            DbOptions options = new DbOptions()
            {
                HrConnectionString = TestDbConnection
            };
            services.AddSingleton<DbOptions>(options);
            services.AddHrMigration(options);
        });

        builder.UseEnvironment("Development");
    }

    private static void RemoveServiceDescriptor<T>(IServiceCollection services)
    {
        ServiceDescriptor? migratorDescriptor =
            services.SingleOrDefault(d => d.ServiceType == typeof(T));

        if (migratorDescriptor != null)
        {
            services.Remove(migratorDescriptor);
        }
    }
}