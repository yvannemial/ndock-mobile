using Mobile.Models;
using Mobile.Services;

namespace Mobile.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        CheckForExistingToken();
    }

    private async void CheckForExistingToken()
    {
        try
        {
            // Show loading indicator while checking token
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            if (await TokenService.Instance.HasValidTokenAsync())
            {
                var (token, tokenType) = await TokenService.Instance.GetTokenAsync();
                ApiService.Instance.SetAuthToken(token!, tokenType!);
                
                // Navigate to the main application page
                Window!.Page = new AppShell();
            }
        }
        catch (Exception ex)
        {
            // If there's an error, we'll just continue with the login page
            Console.WriteLine($"Error checking token: {ex.Message}");
        }
        finally
        {
            // Hide loading indicator
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
        }
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter both email and password.", "OK");
            return;
        }

        try
        {
            // Show loading indicator
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;
            LoginButton.IsEnabled = false;

            // Perform login logic with the API service
            TokenData? loginResult = await ApiService.Instance.LoginAsync(email, password);

            if (loginResult == null)
            {
                await DisplayAlert("Error", "Invalid email or password.", "OK");
                return;
            }

            // Store the token in the API service for future authenticated requests
            ApiService.Instance.SetAuthToken(loginResult.AccessToken, loginResult.TokenType);
            
            // Save token if remember me is checked
            bool rememberMe = RememberMeCheckbox.IsChecked;
            await TokenService.Instance.SaveTokenAsync(loginResult.AccessToken, loginResult.TokenType, rememberMe);

            // Navigate to the main application page
            Window!.Page = new AppShell();
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlert("Error", $"Connection error: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Login failed: {ex.Message}", "OK");
        }
        finally
        {
            // Hide loading indicator
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            LoginButton.IsEnabled = true;
        }
    }

    private async void OnRegisterLinkTapped(object sender, EventArgs e)
    {
        // Navigate to the RegistrationPage
        await Navigation.PushAsync(new RegisterPage());
    }

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        // You can implement the forgot password functionality here
        // For example, navigate to a password reset page or show a dialog
        string email = await DisplayPromptAsync("Password Reset", "Enter your email address to reset your password:",
            "Submit", "Cancel", "Email");

        if (!string.IsNullOrWhiteSpace(email))
        {
            // Send password reset email logic here
            await DisplayAlert("Password Reset",
                "If an account exists with this email, a password reset link will be sent.", "OK");
        }
    }
}