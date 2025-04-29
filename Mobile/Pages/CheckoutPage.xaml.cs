using MapboxMaui;
using MapboxMaui.Styles;
using Mobile.Models;
using Mobile.ViewModels;

namespace Mobile.Pages;

public partial class CheckoutPage : ContentPage
{
    MapboxView _map;
    CameraOptions _cameraOptions;
    
    private readonly CartSheet _cart;
    private readonly CheckoutViewModel _vm;

    public CheckoutPage(Menu menu, OrderItem order)
    {
        InitializeComponent();
        
        Content = _map = new MapboxView();

        _vm = new CheckoutViewModel(menu, order);
        
        _cart = new CartSheet(_vm);
        BindingContext = _vm;
        
        _map.MapReady += Map_MapReady;
        _map.StyleLoaded += Map_StyleLoaded;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _cart.ShowAsync();
        
        // Load user location first, then get delivery estimate
        Task.Run(async () =>
        {
            await _vm.LoadUserLocationAsync();
            await _vm.GetDeliveryEstimateAsync();
            
            _cameraOptions = new CameraOptions
            {
                Center = _vm.DeliveryLocation,
                Zoom = 18,
            };
            
            _map.CameraOptions = _cameraOptions;
            _map.MapboxStyle = MapboxStyle.MAPBOX_STREETS;
            
            const string imageId = @"BLUE_ICON_ID";
            const string imageName = @"blue_marker_view";
            const string sourceId = @"SOURCE_ID";
            var image = new ResolvedImage(imageId, imageName);

            _map.Images = new[] { image };

            var source = new GeoJSONSource(sourceId)
            {
                Data = new GeoJSON.Text.Geometry.Point(_vm.DeliveryLocation)
            };

            _map.Sources = new[] { source };

            var layer = new SymbolLayer(@"LAYER_ID", sourceId)
            {
                IconImage = new PropertyValue<ResolvedImage>(image),
                IconAnchor = IconAnchor.Bottom,
                IconOffset = new PropertyValue<double[]>(new double [] { 0.0, 12.0 }),
            };

            _map.Layers = new[] { layer };
        });
    }

    protected override void OnDisappearing()
    {
        _cart.DismissAsync();
        
        base.OnDisappearing();
    }

    private void Map_StyleLoaded(object sender, EventArgs e)
    {
    }

    private void Map_MapReady(object sender, EventArgs e)
    {
        _map.CameraOptions = _cameraOptions;
        _map.MapboxStyle = MapboxStyle.MAPBOX_STREETS;
    }
}