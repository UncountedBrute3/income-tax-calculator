using System.Data;
using Dapper;
using IncomeTaxCalculator.Domain.Interfaces;
using IncomeTaxCalculator.Infrastructure.Interfaces;

namespace IncomeTaxCalculator.Infrastructure.Repositories;

public class TaxBandRepository
{
    private readonly IDbContext _dbContext;
    
    public TaxBandRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ITaxBand>> GetAll()
    {
        using IDbConnection connection = _dbContext.CreateConnection();
        return await connection.QueryAsync<ITaxBand>(GetAllQuery);
    }

    private const string GetAllQuery = @"
SELECT
    t.Id,
    t.Name,
    t.StartRange,
    t.EndRange,
    t.TaxRate
FROM TaxBands as t
";
}