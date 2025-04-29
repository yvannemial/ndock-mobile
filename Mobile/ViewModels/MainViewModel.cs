using Mobile.Models;
using Mobile.Pages;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Mobile.Services;

namespace Mobile.ViewModels
{
    public class MainViewModel : ObservableModel
    {
        private string _userLocation = "Unknown Location";
        private string _searchQuery = "";
        private ObservableCollection<MenuCategory> _foodCategories = [];
        private ObservableCollection<Restaurant> _restaurants = [];
        private ObservableCollection<Menu> _mostRatedMenus = [];
        private bool _isLoading = false;
        private string _errorMessage = "";

        public MainViewModel()
        {
            // Initialize commands
            ViewAllRestaurantsCommand = new Command(OnViewAllRestaurantsClicked);
            RefreshCommand = new Command(async void () => await LoadDataAsync());

            // Add new command for restaurant selection
            RestaurantSelectedCommand = new Command<Restaurant>(OnRestaurantSelected);

            UserService.Instance.UserChanged +=
                async (sender, user) => UserLocation = user?.Address ?? "Unknown Location";

            // Load data
            Task.Run(async () => await LoadDataAsync());
        }

        public string UserLocation
        {
            get => _userLocation;
            set => SetProperty(ref _userLocation, value);
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
                }
            }
        }

        public ObservableCollection<MenuCategory> FoodCategories
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

        public ObservableCollection<Restaurant> Restaurants
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

        public ObservableCollection<Menu> MostRatedMenus
        {
            get => _mostRatedMenus;
            set
            {
                if (_mostRatedMenus != value)
                {
                    _mostRatedMenus = value;
                    OnPropertyChanged();
                }
            }
        }


        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HasError));
                }
            }
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand ViewAllRestaurantsCommand { get; }
        public ICommand RefreshCommand { get; }

        public ICommand RestaurantSelectedCommand { get; }

        private async Task LoadDataAsync()
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                await Task.WhenAll(
                    LoadFoodCategoriesAsync(),
                    LoadRestaurantsAsync(),
                    LoadMostRatedMenusAsync()
                );
            }
            catch (Exception ex)
            {
                ErrorMessage = "Failed to load data. Please check your connection and try again.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadFoodCategoriesAsync()
        {
            try
            {
                var categories = await ApiService.Instance.GetMenuCategoriesAsync();
                if (categories != null)
                {
                    FoodCategories = new ObservableCollection<MenuCategory>(categories);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading food categories: {ex.Message}");
            }
        }

        private async Task LoadRestaurantsAsync()
        {
            try
            {
                var restaurants = await ApiService.Instance.GetRestaurantsAsync();
                if (restaurants != null)
                {
                    Restaurants = new(restaurants);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading restaurants: {ex.Message}");
            }
        }

        private async Task LoadMostRatedMenusAsync()
        {
            try
            {
                var menus = await ApiService.Instance.GetMostRatedMenusAsync(limit: 6);
                MostRatedMenus = new ObservableCollection<Menu>(menus ?? []);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading most rated menus: {ex.Message}";
            }
        }


        private void OnViewAllRestaurantsClicked()
        {
            // Navigate to all restaurants view
        }

        private async void OnRestaurantSelected(Restaurant restaurant)
        {
            // Navigate to the RestaurantPage, passing the selected restaurant
            await Shell.Current.Navigation.PushAsync(new RestaurantPage(restaurant));
        }
    }
}