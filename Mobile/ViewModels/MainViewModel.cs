using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Mobile.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _userLocation = "Surakarta, ID";
        private string _searchQuery;
        private ObservableCollection<FoodCategoryModel> _foodCategories;
        private ObservableCollection<RestaurantModel> _restaurants;

        public MainViewModel()
        {
            // Initialize commands
            NotificationCommand = new Command(OnNotificationClicked);
            ViewAllRestaurantsCommand = new Command(OnViewAllRestaurantsClicked);
            OrderCommand = new Command<RestaurantModel>(OnOrderClicked);
            InfoCommand = new Command<RestaurantModel>(OnInfoClicked);
            FavoriteCommand = new Command<RestaurantModel>(OnFavoriteClicked);
            ProfileCommand = new Command<RestaurantModel>(OnProfileClicked);

            // Load data
            LoadFoodCategories();
            LoadRestaurants();
        }

        public string UserLocation
        {
            get => _userLocation;
            set
            {
                if (_userLocation != value)
                {
                    _userLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                    // Optionally trigger search functionality
                }
            }
        }

        public ObservableCollection<FoodCategoryModel> FoodCategories
        {
            get => _foodCategories;
            set
            {
                if (_foodCategories != value)
                {
                    _foodCategories = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<RestaurantModel> Restaurants
        {
            get => _restaurants;
            set
            {
                if (_restaurants != value)
                {
                    _restaurants = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NotificationCommand { get; }
        public ICommand ViewAllRestaurantsCommand { get; }
        public ICommand OrderCommand { get; }
        public ICommand InfoCommand { get; }
        public ICommand FavoriteCommand { get; }
        public ICommand ProfileCommand { get; }

        private void LoadFoodCategories()
        {
            // In a real app, this would be loaded from the API
            FoodCategories = new ObservableCollection<FoodCategoryModel>
            {
                new FoodCategoryModel { Id = 1, Name = "Pizza", ImageSource = "pizza_icon.png" },
                new FoodCategoryModel { Id = 2, Name = "Noodle", ImageSource = "noodle_icon.png" },
                new FoodCategoryModel { Id = 3, Name = "Burger", ImageSource = "burger_icon.png" },
                new FoodCategoryModel { Id = 4, Name = "Rice", ImageSource = "rice_icon.png" },
                new FoodCategoryModel { Id = 5, Name = "Salad", ImageSource = "salad_icon.png" }
            };
        }

        private void LoadRestaurants()
        {
            // In a real app, this would be loaded from the API
            Restaurants = new ObservableCollection<RestaurantModel>
            {
                new RestaurantModel
                {
                    Id = 1,
                    Name = "Burger Bansor Surakarta",
                    Address = "885 Ave, Surakarta, ID",
                    ShortAddress = "885 Ave",
                    Rating = 4.5f,
                    ReviewCount = 1500,
                    DeliveryTime = "25 - 35 mins",
                    BannerUrl = "burger_restaurant.jpg"
                },
                new RestaurantModel
                {
                    Id = 2,
                    Name = "Fresh Salad Bar",
                    Address = "762 St, Surakarta, ID",
                    ShortAddress = "762 St",
                    Rating = 4.7f,
                    ReviewCount = 2300,
                    DeliveryTime = "15 - 25 mins",
                    BannerUrl = "salad_restaurant.jpg"
                }
            };
        }

        private void OnNotificationClicked()
        {
            // Handle notification click
        }

        private void OnViewAllRestaurantsClicked()
        {
            // Navigate to all restaurants view
        }

        private void OnOrderClicked(RestaurantModel restaurant)
        {
            // Navigate to order page for the selected restaurant
        }

        private void OnInfoClicked(RestaurantModel restaurant)
        {
            // Show restaurant info
        }

        private void OnFavoriteClicked(RestaurantModel restaurant)
        {
            // Toggle favorite status
        }

        private void OnProfileClicked(RestaurantModel restaurant)
        {
            // Navigate to profile page
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class FoodCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }

    public class RestaurantModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ShortAddress { get; set; }
        public float Rating { get; set; }
        public int ReviewCount { get; set; }
        public string RatingDisplay => $"{Rating} ({ReviewCount / 1000f:0.0}k)";
        public string DeliveryTime { get; set; }
        public string LogoUrl { get; set; }
        public string BannerUrl { get; set; }
    }
}