using System;
using Mobile.Models;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Mobile.Services;

namespace Mobile.Pages
{
    public partial class ProfilePage : ContentPage
    {
        private User _currentUser;
        private bool _isEditMode = false;

        public ProfilePage()
        {
            InitializeComponent();
            LoadUserData();
        }

        private async void LoadUserData()
        {
            try
            {
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                // In a real app, you would fetch this from your API or local storage
                // For now, we'll create a sample user
                _currentUser = UserService.Instance.CurrentUser!;

                // Update UI with user data
                UpdateUserInterface();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load profile: {ex.Message}", "OK");
            }
            finally
            {
                LoadingIndicator.IsRunning = false;
                LoadingIndicator.IsVisible = false;
            }
        }

        private void UpdateUserInterface()
        {
            // Set the binding context for data binding
            BindingContext = _currentUser;

            // Update header labels
            UserNameLabel.Text = $"{_currentUser.FirstName} {_currentUser.LastName}";
            UserEmailLabel.Text = _currentUser.Email;
        }

        private void OnEditProfileClicked(object sender, EventArgs e)
        {
            _isEditMode = !_isEditMode;
            
            // Toggle edit mode
            FirstNameEntry.IsEnabled = _isEditMode;
            LastNameEntry.IsEnabled = _isEditMode;
            EmailEntry.IsEnabled = _isEditMode;
            PhoneNumberEntry.IsEnabled = _isEditMode;
            AddressEntry.IsEnabled = _isEditMode;
            
            // Change button text based on mode
            if (_isEditMode)
            {
                EditProfileButton.Text = "Save Changes";
            }
            else
            {
                EditProfileButton.Text = "Edit Profile";
                SaveUserChanges();
            }
        }

        private async void SaveUserChanges()
        {
            try
            {
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                // Update the user object with values from UI
                _currentUser.FirstName = FirstNameEntry.Text;
                _currentUser.LastName = LastNameEntry.Text;
                _currentUser.Email = EmailEntry.Text;
                _currentUser.PhoneNumber = PhoneNumberEntry.Text;
                _currentUser.Address = AddressEntry.Text;
                _currentUser.UpdatedAt = DateTime.Now;

                // In a real app, you would send this data to your API
                await Task.Delay(1000); // Simulate API call

                // Update UI with the new data
                UpdateUserInterface();
                
                await DisplayAlert("Success", "Profile updated successfully", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to update profile: {ex.Message}", "OK");
            }
            finally
            {
                LoadingIndicator.IsRunning = false;
                LoadingIndicator.IsVisible = false;
            }
        }

        private async void OnChangePasswordClicked(object sender, EventArgs e)
        {
            // This would typically navigate to a password change page
            // or show a dialog to change password
            await DisplayAlert("Change Password", "Password change functionality would be implemented here", "OK");
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            
            if (confirm)
            {
                await (Shell.Current as AppShell)?.LogoutAsync()!;
            }
        }
    }
}