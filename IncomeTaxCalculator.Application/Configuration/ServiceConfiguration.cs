using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Services;
using IncomeTaxCalculator.Application.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeTaxCalculator.Application.Configuration;

public static class ServiceConfiguration
{
    /// <summary>
    /// Method to add services to the <see cref="IServiceCollection"/> of the .NET pipeline.
    /// </summary>
    /// <param name="serviceCollection">The service collection to install into.</param>
    /// <returns>The service collection with the new services installed.</returns>
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IEmployeeExtractionStrategy, EmployeeExtractionStrategy>()
            .AddSingleton<IEmployeeLoadStrategy, EmployeeLoadStrategy>()
            .AddSingleton<IEmployeeTransformStrategy, EmployeeTransformStrategy>()
            .AddSingleton<IProcessingService, ProcessingService>();
    }
}