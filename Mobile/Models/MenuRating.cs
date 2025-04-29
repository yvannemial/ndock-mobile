using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class MenuRating
    {
        [JsonPropertyName("menu_id")]
        public int MenuId { get; set; }

        [JsonPropertyName("average_rating")]
        public float AverageRating { get; set; }

        [JsonPropertyName("total_reviews")]
        public int TotalReviews { get; set; }
    }
}
