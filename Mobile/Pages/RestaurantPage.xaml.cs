using Mobile.Models;
using Mobile.Services;
using Mobile.ViewModels;

namespace Mobile.Pages
{
    public partial class RestaurantPage : ContentPage
    {
        private Restaurant _restaurant;

        public RestaurantPage(Restaurant restaurant)
        {
            InitializeComponent();

            _restaurant = restaurant;

            // Set the ViewModel with the restaurant and API service
            BindingContext = new RestaurantViewModel(restaurant, ApiService.Instance);
        }

        // Your existing event handlers...

        private void OpenMapButton_Clicked(object sender, EventArgs e)
        {
            // Implement map opening logic
            DisplayAlert("Map", $"Opening map at location: {_restaurant.Latitude}, {_restaurant.Longitude}", "OK");
        }

        private async void AddMenuItem_Clicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is Menu menuItem)
            {
                // Add item to cart
                await DisplayAlert("Added to Cart", $"{menuItem.Name} added to cart", "OK");
            }
        }

        private async void FavoriteMenuItem_Clicked(object sender, EventArgs e)
        {
            if ((sender as ImageButton)?.CommandParameter is Menu menuItem)
            {
                // Toggle favorite status
                await DisplayAlert("Favorite", $"{menuItem.Name} added to favorites", "OK");
            }
        }

        // Add a new method to handle tapping on a menu item
        private async void MenuItem_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is Menu menuItem)
            {
                await Navigation.PushAsync(new DishPage(menuItem));
            }
        }
    }
}