using Mobile.Models;
using System;

namespace Mobile.Services
{
    public class UserService
    {
        // Singleton instance
        private static readonly Lazy<UserService> LazyInstance = new Lazy<UserService>(() => new UserService());
        
        public static UserService Instance => LazyInstance.Value;
        
        // Private constructor for a singleton pattern
        private UserService() { }
        
        // Current user property
        private User? _currentUser;
        public User? CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                // Notify subscribers when the user changes
                UserChanged?.Invoke(this, _currentUser);
            }
        }
        
        // Event to notify subscribers when the user changes
        public event EventHandler<User?> UserChanged;
        
        // Helper method to check if user is authenticated
        public bool IsAuthenticated => CurrentUser != null;
    }
}