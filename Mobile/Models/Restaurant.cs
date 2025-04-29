using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class Restaurant : ObservableModel
    {
        private int _id;
        private string _name;
        private string _address;
        private string _phoneNumber;
        private string _description;
        private string _logoUrl;
        private string _bannerUrl;
        private float _latitude;
        private float _longitude;
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
                }
            }
        }

        [JsonPropertyName("address")]
        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ShortAddress));
                }
            }
        }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
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

        [JsonPropertyName("logo_url")]
        public string LogoUrl
        {
            get => _logoUrl;
            set
            {
                if (_logoUrl != value)
                {
                    _logoUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("banner_url")]
        public string BannerUrl
        {
            get => _bannerUrl;
            set
            {
                if (_bannerUrl != value)
                {
                    _bannerUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("latitude")]
        public float Latitude
        {
            get => _latitude;
            set
            {
                if (_latitude != value)
                {
                    _latitude = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonPropertyName("longitude")]
        public float Longitude
        {
            get => _longitude;
            set
            {
                if (_longitude != value)
                {
                    _longitude = value;
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

        // UI helper properties (not in the schema but useful for display)
        [JsonIgnore]
        public string ShortAddress => Address?.Split(',')?.FirstOrDefault()?.Trim() ?? "";

        public Location LatitudeLongitude => new Location(Latitude, Longitude);
    }
}
