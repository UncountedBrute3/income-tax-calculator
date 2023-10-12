namespace IncomeTaxCalculator.Infrastructure.Options;

/// <summary>
/// Class to represent the database options in the appsettings.json file.
/// </summary>
public class DbOptions
{
    /// <summary>
    /// The string used to identify where in appsettings.json the configuration exists.
    /// </summary>
    public const string Databases = "Databases";
    
    public string HrConnectionString { get; set; } = "";
}