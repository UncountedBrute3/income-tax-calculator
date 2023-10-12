using System.Text.Json.Serialization;

namespace IncomeTaxCalculator.Application.Models;

/// <summary>
/// Class to represent the result of the ETL operation.
/// </summary>
public class ExtractDto
{
    /// <summary>
    /// Determines whether the request has been a success.
    /// If any record fails, it is classed as a failure.
    /// </summary>
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess => Succeeded.Count == TotalInput && Failed.Count == 0;
    
    [JsonPropertyName("totalInput")]
    public int TotalInput { get; set; }
    
    [JsonPropertyName("succeeded")]
    public List<int> Succeeded { get; set; } = new();
    
    [JsonPropertyName("failed")]
    public List<int> Failed { get; set; } = new();
}