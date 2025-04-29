using System.Text.RegularExpressions;
using Mobile.Models;
using Mobile.Services;

namespace Mobile.Pages
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            // Set initial visibility
            Step1Layout.IsVisible = true;
            Step2Layout.IsVisible = false;
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (Step2Layout.IsVisible)
            {
                // Go back to step 1
                await SwitchToStep1();
            }
            else
            {
                // Go back to login page
                await Navigation.PopAsync();
            }
        }

        private async void OnLoginLinkTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnTogglePasswordVisibility(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        }

        private void OnToggleConfirmPasswordVisibility(object sender, EventArgs e)
        {
            ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            string firstName = FirstNameEntry.Text;
            string lastName = LastNameEntry.Text;
            string email = EmailEntry.Text;

            // Validate Step 1 inputs
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            // Validate email format    
            if (!IsValidEmail(email))
            {
                await DisplayAlert("Error", "Please enter a valid email address.", "OK");
                return;
            }

            await SwitchToStep2();
        }

        private async Task SwitchToStep1()
        {
            // Animate Step 2 out (slide right and fade out)
            await Task.WhenAll(
                Step2Layout.TranslateTo(1000, 0, 250, Easing.SinInOut),
                Step2Layout.FadeTo(0, 250, Easing.SinInOut)
            );
            Step2Layout.IsVisible = false;

            // Animate Step 1 in (slide in from left and fade in)
            Step1Layout.TranslationX = -1000;
            Step1Layout.Opacity = 0;
            Step1Layout.IsVisible = true;

            await Task.WhenAll(
                Step1Layout.TranslateTo(0, 0, 250, Easing.SinInOut),
                Step1Layout.FadeTo(1, 250, Easing.SinInOut)
            );
        }

        private async Task SwitchToStep2()
        {
            // Animate Step 1 out (slide left and fade out)
            await Task.WhenAll(
                Step1Layout.TranslateTo(-1000, 0, 250, Easing.SinInOut),
                Step1Layout.FadeTo(0, 250, Easing.SinInOut)
            );
            Step1Layout.IsVisible = false;

            // Animate Step 2 in (slide in from right and fade in)
            Step2Layout.TranslationX = 1000;
            Step2Layout.Opacity = 0;
            Step2Layout.IsVisible = true;

            await Task.WhenAll(
                Step2Layout.TranslateTo(0, 0, 250, Easing.SinInOut),
                Step2Layout.FadeTo(1, 250, Easing.SinInOut)
            );
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            string phoneNumber = PhoneNumberEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            // Validate Step 2 inputs
            if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            try
            {
                // Show loading indicator
                if (LoadingIndicator != null)
                {
                    LoadingIndicator.IsVisible = true;
                    LoadingIndicator.IsRunning = true;
                }

                RegisterButton.IsEnabled = false;

                // Create a user object
                var user = new RegisterRequest
                {
                    FirstName = FirstNameEntry.Text,
                    LastName = LastNameEntry.Text,
                    Email = EmailEntry.Text,
                    PhoneNumber = phoneNumber,
                    Password = password
                };

                // Perform registration using ApiService
                var registeredUser = await ApiService.Instance.RegisterAsync(user);

                if (registeredUser != null)
                {
                    ApiService.Instance.SetAuthToken(registeredUser.AccessToken, registeredUser.TokenType);

                    // Navigate to the main application page
                    Window!.Page = new NavigationPage(new AppShell());
                }
                else
                {
                    await DisplayAlert("Error", "Registration failed. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            finally
            {
                // Hide loading indicator
                if (LoadingIndicator != null)
                {
                    LoadingIndicator.IsVisible = false;
                    LoadingIndicator.IsRunning = false;
                }

                RegisterButton.IsEnabled = true;
            }
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }
    }
}