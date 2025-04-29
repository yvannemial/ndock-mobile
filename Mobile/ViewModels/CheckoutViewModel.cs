using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobile.Models;
using Mobile.Services;

namespace Mobile.ViewModels;

public class CheckoutViewModel : ObservableModel
{
    private OrderItem _order;

    private string _estimatedDeliveryTime = "Calculating...";

    public string EstimatedDeliveryTime
    {
        get => _estimatedDeliveryTime;
        set => SetProperty(ref _estimatedDeliveryTime, value);
    }

    private bool _isLoadingEstimate = true;

    public bool IsLoadingEstimate
    {
        get => _isLoadingEstimate;
        set => SetProperty(ref _isLoadingEstimate, value);
    }

    public ObservableCollection<OrderItem> CartItems { get; set; }

    public Menu Menu { get; }

    public float SubTotal { get; }
    public float Total => SubTotal;

    public ICommand CheckoutCommand => new Command(() =>
        App.Current.MainPage.DisplayAlert("Checkout", "Order placed successfully!", "OK"));

    public Restaurant? CurrentRestaurant { get; set; }

    private DeliveryLocation? _deliveryLocation;

    public DeliveryLocation? DeliveryLocation
    {
        get => _deliveryLocation;
        set => SetProperty(ref _deliveryLocation, value);
    }

    private readonly LocationService _locationService;

    public CheckoutViewModel(Menu menu, OrderItem order)
    {
        _locationService = new();

        _order = order;

        Menu = menu;
        CartItems = [order];
        CurrentRestaurant = menu.Restaurant;

        SubTotal = order.Quantity * menu.Price;
        foreach (OrderItemSupplement supplement in order.Supplements)
        {
            Supplement s = menu.Supplements.First(s => s.Id == supplement.SupplementId);
            SubTotal += s.Price * supplement.Quantity;
        }
    }

    public async Task LoadUserLocationAsync()
    {
        try
        {
            // Get the current location from the location service
            var (latitude, longitude) = await _locationService.GetCurrentLocationAsync();

            if (latitude != 0 && longitude != 0)
            {
                // Create a DeliveryLocation object with the current coordinates
                DeliveryLocation = new DeliveryLocation
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    Address = await _locationService.GetAddressFromCoordinatesAsync(latitude, longitude)
                };
            }
            else
            {
                // If location is not available, use a default location or handle accordingly
                DeliveryLocation = new DeliveryLocation
                {
                    // Default values or values from user profile
                    Latitude = 0,
                    Longitude = 0,
                    Address = "Unknown location"
                };
            }
        }
        catch (Exception ex)
        {
            // Handle location service exceptions
            Console.WriteLine($"Error getting location: {ex.Message}");

            // Set a default location
            DeliveryLocation = new DeliveryLocation
            {
                Latitude = 0,
                Longitude = 0,
                Address = "Location unavailable"
            };
        }
    }

    public async Task GetDeliveryEstimateAsync()
    {
        if (CartItems.Count == 0 || CurrentRestaurant == null || DeliveryLocation == null)
        {
            EstimatedDeliveryTime = "N/A";
            IsLoadingEstimate = false;
            return;
        }

        IsLoadingEstimate = true;

        try
        {
            var request = new DeliveryEstimateRequest
            {
                RestaurantId = CurrentRestaurant.Id,
                DeliveryAddress = DeliveryLocation,
                Items = CartItems.ToDictionary(item => item.Menu.Id, item => item.Quantity)
            };

            var response = await ApiService.Instance.GetDeliveryEstimateAsync(request);

            // Format the delivery time (assuming response.EstimatedDeliveryTime is in minutes)
            EstimatedDeliveryTime =
                response != null ? FormatDeliveryTime(DateTime.Now.AddMinutes(response.TotalEstimatedTimeMinutes)) : "Unable to estimate";
        }
        catch (Exception ex)
        {
            EstimatedDeliveryTime = "Error calculating";
            // Log the exception
        }
        finally
        {
            IsLoadingEstimate = false;
        }
    }

    private string FormatDeliveryTime(DateTime estimatedDeliveryTime)
    {
        // Calculate the time difference from now
        TimeSpan timeUntilDelivery = estimatedDeliveryTime - DateTime.Now;

        // Format as time of day
        return $"{estimatedDeliveryTime:h:mm tt}";
    }
}