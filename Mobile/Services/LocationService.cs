namespace Mobile.Services
{
    public class LocationService
    {
        public async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Location Permission Required",
                    "We need access to your location to show nearby restaurants and delivery options. Please grant permission to use this feature.",
                    "OK");
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            return status;
        }

        public async Task<(double Latitude, double Longitude)> GetCurrentLocationAsync()
        {
            try
            {
                PermissionStatus status = await CheckAndRequestLocationPermission();
                if (status != PermissionStatus.Granted)
                    return (0, 0);
                
                Location? location = await Geolocation.Default.GetLastKnownLocationAsync();

                if (location == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                    location = await Geolocation.Default.GetLocationAsync(request);
                }

                return location != null ? (location.Latitude, location.Longitude) :
                    (0, 0);
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device
                return (0, 0);
            }
            catch (PermissionException)
            {
                // Handle permission exception
                return (0, 0);
            }
            catch (Exception)
            {
                // Unable to get location
                return (0, 0);
            }
        }

        public async Task<string> GetAddressFromCoordinatesAsync(double latitude, double longitude)
        {
            try
            {
                var placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    return $"{placemark.Thoroughfare} {placemark.SubThoroughfare}, {placemark.Locality}";
                }

                return "Address unknown";
            }
            catch (Exception)
            {
                return "Address unavailable";
            }
        }
    }
}