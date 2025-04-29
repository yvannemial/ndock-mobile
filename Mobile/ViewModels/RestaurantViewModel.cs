using Mobile.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mobile.ViewModels
{
    public class RestaurantViewModel : ObservableModel
    {
        private Restaurant _restaurant;
        private ObservableCollection<Menu> _menuItems;
        private bool _isLoading;

        public RestaurantViewModel(Restaurant restaurant)
        {
            _restaurant = restaurant;
            _menuItems = new ObservableCollection<Menu>();
            LoadMenuItems();
        }

        public string Name => _restaurant?.Name;
        public string Address => _restaurant?.Address;
        public string ImageUrl => _restaurant?.BannerUrl;
        public string Categories => "Restaurant, Fast Food, Burger"; // This could be fetched from API
        public float Rating => 4.5f; // Replace with actual rating when available
        public string ReviewCount => "1.5k"; // Replace with actual review count when available
        public string DeliveryTime => "30"; // In minutes, could be part of restaurant data
        public string PriceRange => "$$"; // This could be calculated based on menu prices
        
        public ObservableCollection<Menu> MenuItems => _menuItems;
        
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

        private async void LoadMenuItems()
        {
            IsLoading = true;
            
            // Example data - replace with actual API call
            await Task.Delay(500); // Simulate network delay
            
            MenuItems.Add(new Menu 
            { 
                Name = "Cheeseburger", 
                Description = "Classic beef patty with cheese, lettuce, tomato and special sauce",
                Price = 8.99m,
                OriginalPrice = 10.99m,
                HasDiscount = true,
                ImageUrl = "https://example.com/cheeseburger.jpg"
            });
            
            MenuItems.Add(new Menu 
            { 
                Name = "Chicken Wings", 
                Description = "Crispy wings with choice of sauce",
                Price = 7.99m,
                HasDiscount = false,
                ImageUrl = "https://example.com/wings.jpg"
            });
            
            // Add more menu items as needed
            
            IsLoading = false;
        }
    }
}