using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class TokenData
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "bearer";
    }
}
