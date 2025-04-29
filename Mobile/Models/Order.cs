using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class OrderItemSupplement
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("order_item_id")]
        public int OrderItemId { get; set; }
        
        [JsonIgnore]
        public Supplement? Supplement { get; set; }
        
        [JsonPropertyName("supplement_id")]
        public int SupplementId { get; set; }
        
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class Order
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("client_id")]
        public int ClientId { get; set; }

        [JsonPropertyName("items")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}