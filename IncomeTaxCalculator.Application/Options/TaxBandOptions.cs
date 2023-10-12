using IncomeTaxCalculator.Domain.Models;

namespace IncomeTaxCalculator.Application.Options;

public class TaxBandOptions
{
    public const string TaxBand = "TaxBandConfiguration";
    public TaxBand[]? TaxBands { get; set; }
}