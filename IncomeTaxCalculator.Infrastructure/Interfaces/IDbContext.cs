using System.Data;

namespace IncomeTaxCalculator.Infrastructure.Interfaces;

public interface IDbContext
{
    IDbConnection CreateConnection();
}