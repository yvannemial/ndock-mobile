using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public required string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public required string LastName { get; set; }

        [JsonPropertyName("email")]
        public required string Email { get; set; }
        
        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("phone_number")]
        public required string PhoneNumber { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
