using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class HealthCheck
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        [JsonPropertyName("database")]
        public string Database { get; set; }
    }
}
