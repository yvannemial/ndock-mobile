using System;
using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class MenuCategory : ObservableModel
    {
        private int _id;
        private string _name;
        private DateTime _createdAt;
        private DateTime _updatedAt;

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
                    OnPropertyChanged(nameof(ImageSource));
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

        // UI helper property (not in the schema)
        [JsonIgnore]
        public string ImageSource => $"{Name?.ToLower()}_icon.png";
    }
}
