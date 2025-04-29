using Mobile.Models;
using Mobile.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Mobile.ViewModels
{
    public class RestaurantViewModel : ObservableModel
    {
        private readonly ApiService _apiService;
        private bool _isLoading = false;
        private string _errorMessage = String.Empty;

        public Location RestaurantLatitudeLongitude =>
            new Location(Restaurant?.Latitude ?? 0, Restaurant?.Longitude ?? 0);

        public RestaurantViewModel(Restaurant restaurant, ApiService apiService)
        {
            Restaurant = restaurant;
            _apiService = apiService;
            MenuItems = new ObservableCollection<Menu>();

            // Load the menu items when the view model is created
            LoadMenuItemsAsync();
        }

        public Restaurant Restaurant { get; }

        public string ImageUrl => Restaurant.BannerUrl;

        public ObservableCollection<Menu> MenuItems { get; }

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
                }
            }
        }

        private async void LoadMenuItemsAsync()
        {
            if (Restaurant.Id <= 0)
            {
                ErrorMessage = "Invalid restaurant information";
                return;
            }

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                // Call the API service to get the menus for this restaurant
                var menus = await _apiService.GetRestaurantMenusAsync(Restaurant.Id);

                if (menus != null)
                {
                    // Clear any existing items and add the new ones
                    MenuItems.Clear();
                    foreach (Menu menu in menus)
                    {
                        MenuItems.Add(menu);
                    }
                }
                else
                {
                    // Handle the case where no menus are returned
                    ErrorMessage = "No menu items available for this restaurant";
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                ErrorMessage = $"Error loading menu items: {ex.Message}";
                Debug.WriteLine($"Error in LoadMenuItemsAsync: {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        // Method to manually refresh the menu items
        public async Task RefreshMenuItemsAsync()
        {
            await Task.Run(LoadMenuItemsAsync);
        }
    }
}