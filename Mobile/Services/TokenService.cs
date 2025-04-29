using System.Threading.Tasks;

namespace Mobile.Services
{
    public class TokenService
    {
        private static readonly Lazy<TokenService> LazyInstance = new Lazy<TokenService>(() => new TokenService());

        public static TokenService Instance => LazyInstance.Value;

        private const string tokenKey = "auth_token";
        private const string tokenTypeKey = "token_type";
        private const string rememberMeKey = "remember_me";

        private TokenService()
        {
        }

        public async Task SaveTokenAsync(string token, string tokenType, bool rememberMe)
        {
            await SecureStorage.Default.SetAsync(tokenKey, token);
            await SecureStorage.Default.SetAsync(tokenTypeKey, tokenType);
            await SecureStorage.Default.SetAsync(rememberMeKey, rememberMe.ToString());
        }

        public async Task<(string? Token, string? TokenType)> GetTokenAsync()
        {
            string? token = await SecureStorage.Default.GetAsync(tokenKey);
            string? tokenType = await SecureStorage.Default.GetAsync(tokenTypeKey);
            return (token, tokenType);
        }

        public async Task<bool> IsRememberMeEnabledAsync()
        {
            string? rememberMe = await SecureStorage.Default.GetAsync(rememberMeKey);
            return !string.IsNullOrEmpty(rememberMe) && bool.Parse(rememberMe);
        }

        public async Task ClearTokenAsync()
        {
            SecureStorage.Default.Remove(tokenKey);
            SecureStorage.Default.Remove(tokenTypeKey);
            SecureStorage.Default.Remove(rememberMeKey);
            await Task.CompletedTask;
        }

        public async Task<bool> HasValidTokenAsync()
        {
            var (token, tokenType) = await GetTokenAsync();
            bool rememberMeEnabled = await IsRememberMeEnabledAsync();

            return rememberMeEnabled && !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(tokenType);
        }
    }
}