using Mobile.Models;

namespace Mobile.Pages;

public partial class DishPage : ContentPage
{
    private Menu _dish;
    private readonly Dictionary<int, int> _supplementQuantities = new Dictionary<int, int>();
    private readonly HashSet<int> _selectedSupplementIds = new HashSet<int>();
    private float _totalPrice = 0;

    public DishPage()
    {
        InitializeComponent();
    }

    public DishPage(Menu dish) : this()
    {
        _dish = dish;
        BindingContext = _dish;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Initialize the total price
        if (BindingContext is Menu menu)
        {
            _totalPrice = menu.Price;
            UpdateTotalPriceDisplay();

            // Initialize the visibility of quantity controls
            foreach (var supplement in menu.Supplements)
            {
                _supplementQuantities[supplement.Id] = 0;
            }
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void FavoriteButton_Clicked(object sender, EventArgs e)
    {
        // Toggle favorite status
        var button = (Button)sender;

        // This is a placeholder - you would need to implement the actual favorite functionality
        button.ImageSource = button.ImageSource.ToString()!.Contains("favorite_outline.png") ? "favorite_filled.png" : "favorite_outline.png";
    }

    private async void AddToCartButton_Clicked(object sender, EventArgs e)
    {
        // Get the current menu item
        if (BindingContext is not Menu menuItem) return;

        // Create a cart item with the selected supplements
        var cartItem = new OrderItem
        {
            Menu = menuItem,
            MenuId = menuItem.Id,
            Quantity = 1, // Default quantity or use a quantity selector
        };

        // Add selected supplements with their quantities
        foreach (int supplementId in _selectedSupplementIds)
        {
            if (_supplementQuantities.TryGetValue(supplementId, out int quantity) && quantity > 0)
            {
                cartItem.Supplements.Add(new OrderItemSupplement
                {
                    Supplement = menuItem.Supplements.FirstOrDefault(s => s.Id == supplementId),
                    SupplementId = supplementId,
                    Quantity = quantity
                });
            }
        }

        // Add to cart logic here
        // ...

        // Navigate to the CheckoutPage
        await Navigation.PushAsync(new CheckoutPage(menuItem, cartItem));
    }

    private void OnSupplementCheckChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is not CheckBox { BindingContext: int supplementId } box || BindingContext is not Menu menu) return;

        var supplement = menu.Supplements?.FirstOrDefault(s => s.Id == supplementId);
        if (supplement == null) return;

        // Get the parent grid
        if (box.Parent is not Grid grid) return;

        // Find the quantity controls in the same row
        var quantityControls = grid.FindByName<HorizontalStackLayout>("QuantityControls");
        var quantityLabel = grid.FindByName<Label>("QuantityLabel");

        if (quantityControls == null || quantityLabel == null) return;

        if (e.Value) // Checkbox is checked
        {
            // Show quantity controls
            quantityControls.IsVisible = true;

            if (_selectedSupplementIds.Add(supplementId))
            {
                // Set quantity to 1 if it was 0
                if (!_supplementQuantities.ContainsKey(supplementId) || _supplementQuantities[supplementId] == 0)
                {
                    _supplementQuantities[supplementId] = 1;
                    quantityLabel.Text = "1";
                    _totalPrice += supplement.Price;
                }
            }
        }
        else // Checkbox is unchecked
        {
            // Hide quantity controls
            quantityControls.IsVisible = false;

            if (_selectedSupplementIds.Contains(supplementId))
            {
                _selectedSupplementIds.Remove(supplementId);

                // Subtract the price for all quantities of this supplement
                if (_supplementQuantities.TryGetValue(supplementId, out var quantity))
                {
                    _totalPrice -= supplement.Price * quantity;
                    _supplementQuantities[supplementId] = 0;
                    quantityLabel.Text = "0";
                }
            }
        }

        UpdateTotalPriceDisplay();
    }

    private void IncrementSupplementQuantity_Clicked(object sender, EventArgs e)
    {
        if (sender is not Button { BindingContext: int supplementId } button || BindingContext is not Menu menu) return;

        var supplement = menu.Supplements.FirstOrDefault(s => s.Id == supplementId);
        if (supplement == null) return;

        // Get the parent grid
        if (button.Parent?.Parent is not Grid grid) return;

        // Find the quantity label in the same row
        var quantityLabel = grid.FindByName<Label>("QuantityLabel");
        if (quantityLabel == null) return;

        // Update the quantity
        _supplementQuantities.TryAdd(supplementId, 0);

        _supplementQuantities[supplementId]++;
        quantityLabel.Text = _supplementQuantities[supplementId].ToString();

        // If this is the first increment, also check the checkbox
        if (_supplementQuantities[supplementId] == 1)
        {
            var checkBox = grid.Children.OfType<CheckBox>().FirstOrDefault();
            if (checkBox is { IsChecked: false })
            {
                checkBox.IsChecked = true;
            }
        }

        _totalPrice += supplement.Price;
        UpdateTotalPriceDisplay();
    }

    private void DecrementSupplementQuantity_Clicked(object sender, EventArgs e)
    {
        if (sender is not Button { BindingContext: int supplementId } button || BindingContext is not Menu menu) return;

        var supplement = menu.Supplements?.FirstOrDefault(s => s.Id == supplementId);
        if (supplement == null) return;

        // Get the parent grid
        if (button.Parent?.Parent is not Grid grid) return;

        // Find the quantity label in the same row
        var quantityLabel = grid.FindByName<Label>("QuantityLabel");
        if (quantityLabel == null) return;

        // Update the quantity if greater than 0
        if (!_supplementQuantities.TryGetValue(supplementId, out var currentQuantity) || currentQuantity <= 0) return;

        _supplementQuantities[supplementId]--;
        quantityLabel.Text = _supplementQuantities[supplementId].ToString();

        // If quantity becomes 0, uncheck the checkbox
        if (_supplementQuantities[supplementId] == 0)
        {
            var checkBox = grid.Children.OfType<CheckBox>().FirstOrDefault();
            if (checkBox is { IsChecked: true })
            {
                checkBox.IsChecked = false;
            }
        }

        _totalPrice -= supplement.Price;
        UpdateTotalPriceDisplay();
    }

    private void UpdateTotalPriceDisplay()
    {
        // Update the total price label
        if (TotalPriceLabel != null)
        {
            TotalPriceLabel.Text = $"{_totalPrice} XAF";
        }
    }
}