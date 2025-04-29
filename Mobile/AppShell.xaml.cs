using Mobile.Pages;
using Mobile.Services;
using System.Diagnostics;

namespace Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        // Load user data when the shell initializes
        LoadUserDataAsync();
    }
    
    private async void LoadUserDataAsync()
    {
        try
        {
            // Get the current user data from the API
            var user = await ApiService.Instance.GetCurrentUserAsync();
            
            if (user != null)
            {
                // Store the user in the application-wide UserService
                UserService.Instance.CurrentUser = user;
                
                // You can also set user-specific properties or update UI elements here
                Debug.WriteLine($"User loaded: {user.FirstName} {user.LastName}");
            }
            else
            {
                Debug.WriteLine("Failed to load user data");
                // Handle the case where user data couldn't be retrieved
                // This might indicate an authentication issue
                await LogoutAsync();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading user data: {ex.Message}");
            // Handle exceptions, possibly logging out the user if there's an authentication error
            await LogoutAsync();
        }
    }
    
    public async Task LogoutAsync()
    {
        // Clear the stored token
        await TokenService.Instance.ClearTokenAsync();
        
        // Clear the current user data
        UserService.Instance.CurrentUser = null;
        
        // Navigate back to login page
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}