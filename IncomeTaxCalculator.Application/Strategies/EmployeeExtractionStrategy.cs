using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using Microsoft.Extensions.Logging;

namespace IncomeTaxCalculator.Application.Strategies;

/// <summary>
/// <para>Class to handle the extraction of employee data.</para>
/// <para>This implementation is designed to handle CSV.</para>
/// </summary>
public class EmployeeExtractionStrategy : IEmployeeExtractionStrategy
{
    private readonly ILogger<EmployeeExtractionStrategy> _logger;
    
    public EmployeeExtractionStrategy(ILogger<EmployeeExtractionStrategy> logger)
    {
        _logger = logger;
    }
    
    /// <summary>
    /// Method to extract a CSV file into instances of <see cref="EmployeeExtract"/>.
    /// </summary>
    /// <param name="data">The CSV file to extract.</param>
    /// <returns>The list of extracted employees.</returns>
    public IEnumerable<EmployeeExtract> Extract(Stream data)
    {
        // Reset the stream in-case another method has read it.
        data.Seek(0, SeekOrigin.Begin);

        var conf = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            BadDataFound = context =>
            {
                // When bad data is identified, log it so it's easier to identify.
                // CSV Helper will not expose errors if bad data does occur unless this is used.
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