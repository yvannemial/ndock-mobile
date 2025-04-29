using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class Menu : ObservableModel
    {
        private int _id;
        private int _restaurantId;
        private List<int> _categoryIds;
        private string _name;
        private float _price;
        private float? _average_rating;
        private string _description;
        private int _preparationTime;
        private string? _imageUrl;
        private DateTime _createdAt;
        private DateTime _updatedAt;
        private List<MenuCategory> _categories;
        private Restaurant? _restaurant;
        private List<Supplement> _supplements;

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

        [JsonPropertyName("category_ids")]
        public List<int> CategoryIds
        {
            get => _categoryIds;
            set
            {
                if (_categoryIds != value)
                {
                    _categoryIds = value;
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
                if (Math.Abs(_price - value) > float.Epsilon)
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
        public string? ImageUrl
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

        [JsonPropertyName("categories")]
        public List<MenuCategory> Categories
        {
            get => _categories;
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("average_rating")]
        public float? AverageRating
        {
            get => _average_rating;
            set
            {
                if (_average_rating != value)
                {
                    _average_rating = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("restaurant")]
        public Restaurant? Restaurant
        {
            get => _restaurant;
            set
            {
                if (_restaurant != value)
                {
                    _restaurant = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("supplements")]
        public List<Supplement> Supplements
        {
            get => _supplements;
            set
            {
                if (_supplements != value)
                {
                    _supplements = value;
                    OnPropertyChanged();
                }
            }
        }

        public Menu()
        {
            _categoryIds = new List<int>();
            _categories = new List<MenuCategory>();
            _supplements = new List<Supplement>();
        }
    }
}