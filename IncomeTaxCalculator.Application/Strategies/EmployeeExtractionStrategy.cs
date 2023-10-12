using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using Microsoft.Extensions.Logging;

namespace IncomeTaxCalculator.Application.Strategies;

public class EmployeeExtractionStrategy : IEmployeeExtractionStrategy
{
    private readonly ILogger<EmployeeExtractionStrategy> _logger;
    
    public EmployeeExtractionStrategy(ILogger<EmployeeExtractionStrategy> logger)
    {
        _logger = logger;
    }
    
    public IEnumerable<EmployeeExtract> Extract(Stream data)
    {
        data.Seek(0, SeekOrigin.Begin);

        var conf = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            BadDataFound = context =>
            {
                _logger.LogWarning("Bad data detected {record}.", context.RawRecord);
            }
        };
        using (var reader = new StreamReader(data, Encoding.UTF8, true))
        using (var csv = new CsvReader(reader, conf))
        {
            IEnumerable<EmployeeExtract> records = csv.GetRecords<EmployeeExtract>();
            return records.ToArray();
        }
    }
}