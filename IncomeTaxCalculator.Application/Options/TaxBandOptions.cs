using IncomeTaxCalculator.Domain.Models;

namespace IncomeTaxCalculator.Application.Options;

/// <summary>
/// Class to represent the Tax Bands option in the appsettings.json file.
/// </summary>
public class TaxBandOptions
{
    /// <summary>
    /// The string used to identify where in appsettings.json the configuration exists.
    /// </summary>
    public const string TaxBand = "TaxBandConfiguration";
    
    /// <summary>
    /// The list of <see cref="TaxBand"/> that can be used to calculate net income.
    /// </summary>
    public TaxBand[]? TaxBands { get; set; }
}