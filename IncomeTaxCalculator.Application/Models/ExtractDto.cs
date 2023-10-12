using System.Text.Json.Serialization;

namespace IncomeTaxCalculator.Application.Models;

public class ExtractDto
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess => Succeeded.Count == TotalInput && Failed.Count == 0;
    
    [JsonPropertyName("totalInput")]
    public int TotalInput { get; set; }
    
    [JsonPropertyName("succeeded")]
    public List<int> Succeeded { get; set; } = new();
    
    [JsonPropertyName("failed")]
    public List<int> Failed { get; set; } = new();
}