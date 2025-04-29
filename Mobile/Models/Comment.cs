using System;
using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class Comment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("menu_id")]
        public int MenuId { get; set; }

        [JsonPropertyName("client_id")]
        public int ClientId { get; set; }

        [JsonPropertyName("comment")]
        public string CommentText { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
