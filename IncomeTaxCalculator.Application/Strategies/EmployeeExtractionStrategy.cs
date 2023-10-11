using System.Globalization;
using CsvHelper;
using IncomeTaxCalculator.Application.Interfaces;

namespace IncomeTaxCalculator.Application.Strategies;

public class EmployeeExtractionStrategy : IEmployeeExtractionStrategy
{
    public IAsyncEnumerable<IEmployeeExtract> Extract(Stream data)
    {
        using (var reader = new StreamReader(data))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            IAsyncEnumerable<IEmployeeExtract> records = csv.GetRecordsAsync<IEmployeeExtract>();
            return records;
        }
    }
}