using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Services;
using IncomeTaxCalculator.Application.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeTaxCalculator.Application.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IEmployeeExtractionStrategy, EmployeeExtractionStrategy>()
            .AddSingleton<IEmployeeLoadStrategy, EmployeeLoadStrategy>()
            .AddSingleton<IEmployeeTransformStrategy, EmployeeTransformStrategy>()
            .AddSingleton<IProcessingService, ProcessingService>();
    }
}