using IncomeTaxCalculator.Infrastructure.Interfaces;
using IncomeTaxCalculator.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeTaxCalculator.Infrastructure.Configurations;

/// <summary>
/// Class to handle configuration of repositories.
/// </summary>
public static class RepositoryConfiguration
{
    /// <summary>
    /// Method to handle configuration of repositories.
    /// </summary>
    /// <param name="serviceCollection">The service collection to extend.</param>
    /// <returns>An extended service collection.</returns>
    public static IServiceCollection ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IEmployeeRepository, EmployeeRepository>();
    }
}