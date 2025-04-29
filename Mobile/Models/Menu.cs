using System;
using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class Menu : ObservableModel
    {
        private int _id;
        private int _restaurantId;
        private int _categoryId;
        private string _name;
        private float _price;
        private string _description;
        private int _preparationTime;
        private string _imageUrl;
        private DateTime _createdAt;
        private DateTime _updatedAt;
        private MenuCategory _category;

        [JsonPropertyName("id")]
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("restaurant_id")]
        public int RestaurantId
        {
            get => _restaurantId;
            set
            {
                if (_restaurantId != value)
                {
                    _restaurantId = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("category_id")]
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("price")]
        public float Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("description")]
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("preparation_time")]
        public int PreparationTime
        {
            get => _preparationTime;
            set
            {
                if (_preparationTime != value)
                {
                    _preparationTime = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("image_url")]
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (_imageUrl != value)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                if (_createdAt != value)
                {
                    _createdAt = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt
        {
            get => _updatedAt;
            set
            {
                if (_updatedAt != value)
                {
                    _updatedAt = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("category")]
        public MenuCategory Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
