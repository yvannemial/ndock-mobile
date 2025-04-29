using System;
using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class EstimateResponse
    {
        [JsonPropertyName("restaurant_name")]
        public string RestaurantName { get; set; }

        [JsonPropertyName("restaurant_address")]
        public string RestaurantAddress { get; set; }

        [JsonPropertyName("delivery_address")]
        public string DeliveryAddress { get; set; }

        [JsonPropertyName("distance_km")]
        public float DistanceKm { get; set; }

        [JsonPropertyName("preparation_time_minutes")]
        public int PreparationTimeMinutes { get; set; }

        [JsonPropertyName("estimated_delivery_duration_minutes")]
        public float EstimatedDeliveryDurationMinutes { get; set; }

        [JsonPropertyName("total_estimated_time_minutes")]
        public float TotalEstimatedTimeMinutes { get; set; }

        [JsonPropertyName("estimated_delivery_time")]
        public DateTime EstimatedDeliveryTime { get; set; }

        [JsonPropertyName("total_order_price")]
        public float TotalOrderPrice { get; set; }
    }
}
