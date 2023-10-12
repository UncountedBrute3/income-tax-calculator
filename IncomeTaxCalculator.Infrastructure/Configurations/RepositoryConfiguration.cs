using IncomeTaxCalculator.Infrastructure.Interfaces;
using IncomeTaxCalculator.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeTaxCalculator.Infrastructure.Configurations;

public static class RepositoryConfiguration
{
    public static IServiceCollection ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IEmployeeRepository, EmployeeRepository>();
    }
}