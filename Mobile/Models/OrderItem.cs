using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class OrderItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }
        
        [JsonPropertyName("menu_id")]
        public int MenuId { get; set; }
        
        [JsonIgnore]
        public Menu? Menu { get; set; }
        
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        
        [JsonPropertyName("supplements")]
        public List<OrderItemSupplement> Supplements { get; set; } = new List<OrderItemSupplement>();
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
