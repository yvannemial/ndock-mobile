using System.Text.Json.Serialization;
using GeoJSON.Text.Geometry;

namespace Mobile.Models
{
    public class LoginRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterRequest
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;
        
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;
        
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
    }

    public class DeliveryLocation : IPosition
    {
        [JsonIgnore]
        public double? Altitude => null;

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        
        [JsonPropertyName("address")]
        public string Address { get; set; }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }
    }
    
    public class DeliveryEstimateRequest
    {
        [JsonPropertyName("restaurant_id")]
        public int RestaurantId { get; set; }
        
        [JsonPropertyName("delivery_location")]
        public DeliveryLocation DeliveryAddress { get; set; }
        
        [JsonPropertyName("menu_items")]
        public Dictionary<int, int> Items { get; set; } = new ();
    }

    public class OrderItemSupplementRequest
    {
        [JsonPropertyName("supplement_id")]
        public int SupplementId { get; set; }
        
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

    public class OrderItemRequest
    {
        [JsonPropertyName("menu_id")]
        public int MenuId { get; set; }
        
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        
        [JsonPropertyName("supplements")]
        public List<OrderItemSupplementRequest> Supplements { get; set; } = new List<OrderItemSupplementRequest>();
    }

    public class CreateOrderRequest
    {
        [JsonPropertyName("client_id")]
        public int ClientId { get; set; }
        
        [JsonPropertyName("items")]
        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
    }

    public class CreateCommentRequest
    {
        [JsonPropertyName("menu_id")]
        public int MenuId { get; set; }
        
        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;
        
        [JsonPropertyName("rating")]
        public int Rating { get; set; }
    }
}