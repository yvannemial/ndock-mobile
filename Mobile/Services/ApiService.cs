using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Mobile.Models;

namespace Mobile.Services
{
    public class ApiService
    {
        private static readonly Lazy<ApiService> LazyInstance = new Lazy<ApiService>(() => new ApiService());

        public static ApiService Instance => LazyInstance.Value;
        
        private readonly HttpClient _httpClient;
        private string? _authToken;

        private ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://ndock-api.onrender.com/");
        }

        public void SetAuthToken(string token, string tokenType = "Bearer")
        {
            _authToken = $"{tokenType} {token}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, token);
        }

        public void ClearAuthToken()
        {
            _authToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        // Health Check
        public async Task<HealthCheck?> GetHealthCheckAsync()
        {
            return await GetAsync<HealthCheck>("health");
        }

        // Authentication Endpoints
        public async Task<TokenData?> LoginAsync(string email, string password)
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            return await PostAsync<LoginRequest, TokenData>("auth/login", loginRequest);
        }

        public async Task<TokenData?> RegisterAsync(RegisterRequest registerRequest)
        {
            return await PostAsync<RegisterRequest, TokenData>("auth/register", registerRequest);
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            return await GetAsync<User>("auth/me");
        }

        // Restaurants Endpoints
        public async Task<List<Restaurant>?> GetRestaurantsAsync(int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Restaurant>>($"restaurants{query}");
        }

        public async Task<Restaurant?> GetRestaurantAsync(int restaurantId)
        {
            return await GetAsync<Restaurant>($"restaurants/{restaurantId}");
        }

        public async Task<Restaurant?> CreateRestaurantAsync(Restaurant restaurant)
        {
            return await PostAsync<Restaurant, Restaurant>("restaurants", restaurant);
        }

        public async Task<Restaurant?> UpdateRestaurantAsync(int restaurantId, Restaurant restaurant)
        {
            return await PutAsync<Restaurant, Restaurant>($"restaurants/{restaurantId}", restaurant);
        }

        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            await DeleteAsync($"restaurants/{restaurantId}");
        }

        // Restaurant Menus Endpoint
        public async Task<List<Menu>?> GetRestaurantMenusAsync(int restaurantId, int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Menu>>($"restaurants/{restaurantId}/menus{query}");
        }

        // Menus Endpoints
        public async Task<List<Menu>?> GetMenusAsync(int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Menu>>($"menus{query}");
        }

        public async Task<Menu?> GetMenuAsync(int menuId)
        {
            return await GetAsync<Menu>($"menus/{menuId}");
        }

        public async Task<Menu?> CreateMenuAsync(Menu menu)
        {
            return await PostAsync<Menu, Menu>("menus", menu);
        }

        public async Task<Menu?> UpdateMenuAsync(int menuId, Menu menu)
        {
            return await PutAsync<Menu, Menu>($"menus/{menuId}", menu);
        }

        public async Task DeleteMenuAsync(int menuId)
        {
            await DeleteAsync($"menus/{menuId}");
        }

        public async Task<List<Menu>?> GetMostRatedMenusAsync(int skip = 0, int limit = 10)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Menu>>($"menus/most-rated{query}");
        }

        // Menu Categories Endpoints
        public async Task<List<MenuCategory>?> GetMenuCategoriesAsync(int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<MenuCategory>>($"menu-categories{query}");
        }

        public async Task<MenuCategory?> CreateMenuCategoryAsync(MenuCategory menuCategory)
        {
            return await PostAsync<MenuCategory, MenuCategory>("menu-categories", menuCategory);
        }

        public async Task<MenuCategory?> GetMenuCategoryAsync(int categoryId)
        {
            return await GetAsync<MenuCategory>($"menu-categories/{categoryId}");
        }

        public async Task<MenuCategory?> UpdateMenuCategoryAsync(int categoryId, MenuCategory menuCategory)
        {
            return await PutAsync<MenuCategory, MenuCategory>($"menu-categories/{categoryId}", menuCategory);
        }

        public async Task DeleteMenuCategoryAsync(int categoryId)
        {
            await DeleteAsync($"menu-categories/{categoryId}");
        }

        // Supplements Endpoints
        public async Task<List<Supplement>?> GetSupplementsAsync(int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Supplement>>($"supplements{query}");
        }

        public async Task<Supplement?> CreateSupplementAsync(Supplement supplement)
        {
            return await PostAsync<Supplement, Supplement>("supplements", supplement);
        }

        public async Task<Supplement?> GetSupplementAsync(int supplementId)
        {
            return await GetAsync<Supplement>($"supplements/{supplementId}");
        }

        public async Task<Supplement?> UpdateSupplementAsync(int supplementId, Supplement supplement)
        {
            return await PutAsync<Supplement, Supplement>($"supplements/{supplementId}", supplement);
        }

        public async Task DeleteSupplementAsync(int supplementId)
        {
            await DeleteAsync($"supplements/{supplementId}");
        }

        // Comments Endpoints
        public async Task<List<Comment>?> GetCommentsAsync(int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Comment>>($"comments{query}");
        }

        public async Task<Comment?> CreateCommentAsync(CreateCommentRequest request)
        {
            return await PostAsync<CreateCommentRequest, Comment>("comments", request);
        }

        public async Task<Comment?> GetCommentAsync(int commentId)
        {
            return await GetAsync<Comment>($"comments/{commentId}");
        }

        public async Task<Comment?> UpdateCommentAsync(int commentId, CreateCommentRequest request)
        {
            return await PutAsync<CreateCommentRequest, Comment>($"comments/{commentId}", request);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            await DeleteAsync($"comments/{commentId}");
        }

        public async Task<List<Comment>?> GetMenuCommentsAsync(int menuId, int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Comment>>($"menus/{menuId}/comments{query}");
        }

        // Orders Endpoints
        public async Task<List<Order>?> GetOrdersAsync(int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Order>>($"orders{query}");
        }

        public async Task<Order?> CreateOrderAsync(CreateOrderRequest request)
        {
            return await PostAsync<CreateOrderRequest, Order>("orders", request);
        }

        public async Task<Order?> GetOrderAsync(int orderId)
        {
            return await GetAsync<Order>($"orders/{orderId}");
        }

        public async Task<Order?> UpdateOrderAsync(int orderId, CreateOrderRequest request)
        {
            return await PutAsync<CreateOrderRequest, Order>($"orders/{orderId}", request);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await DeleteAsync($"orders/{orderId}");
        }

        // Get orders for a specific user
        public async Task<List<Order>?> GetUserOrdersAsync(int userId, int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Order>>($"users/{userId}/orders{query}");
        }

        // Get orders for a specific restaurant
        public async Task<List<Order>?> GetRestaurantOrdersAsync(int restaurantId, int skip = 0, int limit = 100)
        {
            var query = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Order>>($"restaurants/{restaurantId}/orders{query}");
        }

        // Delivery Estimate
        public async Task<EstimateResponse?> GetDeliveryEstimateAsync(DeliveryEstimateRequest request)
        {
            return await PostAsync<DeliveryEstimateRequest, EstimateResponse>("delivery-estimate", request);
        }

        // Menu Ratings
        public async Task<MenuRating?> GetMenuRatingAsync(int menuId)
        {
            return await GetAsync<MenuRating>($"menus/{menuId}/rating");
        }

        // Base Methods for HTTP Requests
        private async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await ParseResponse<T>(response);
        }

        private async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            return await ParseResponse<TResponse>(response);
        }

        private async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            return await ParseResponse<TResponse>(response);
        }

        private async Task DeleteAsync(string endpoint)
        {
            await _httpClient.DeleteAsync(endpoint);
        }

        private static async Task<T?> ParseResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                // Handle error response and throw as needed.
                return default;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        private string BuildQueryString(Dictionary<string, string>? parameters)
        {
            if (parameters == null || !parameters.Any())
                return string.Empty;

            return "?" + string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
        }
    }
}