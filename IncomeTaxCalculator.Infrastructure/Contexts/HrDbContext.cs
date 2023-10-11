using System.Data;
using IncomeTaxCalculator.Infrastructure.Interfaces;
using IncomeTaxCalculator.Infrastructure.Options;
using Microsoft.Data.SqlClient;

namespace IncomeTaxCalculator.Infrastructure.Contexts;

public class HrDbContext : IDbContext
{
    private readonly string _connectionString;
    
    public HrDbContext(DbOptions options)
    {
        _connectionString = options.HrConnectionString;
    }
    
    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}