using System.Text.Json.Serialization;

namespace ApiWorkshop.Application.Domain.Utils;

public class DefaultResponse<T>
{
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; } = 200;
    [JsonPropertyName("data")]
    public T? Data { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("success")]
    public bool Success { get; set; } = true;
    [JsonPropertyName("count")]
    public int? Count { get; set; } = 0;
    [JsonPropertyName("totalCount")]
    public int? TotalCount { get; set; } = 0;
}