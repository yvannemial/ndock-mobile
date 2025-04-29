using System;
using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class Supplement : ObservableModel
    {
        private int _id;
        private string _name;
        private float _price;
        private string _description;
        private int _preparationTime;
        private string? _imageUrl;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        [JsonPropertyName("id")]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        [JsonPropertyName("price")]
        public float Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        [JsonPropertyName("description")]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        [JsonPropertyName("preparation_time")]
        public int PreparationTime
        {
            get => _preparationTime;
            set => SetProperty(ref _preparationTime, value);
        }

        [JsonPropertyName("image_url")]
        public string? ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt
        {
            get => _updatedAt;
            set => SetProperty(ref _updatedAt, value);
        }
    }
}