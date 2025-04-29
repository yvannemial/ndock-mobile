using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://127.0.0.1:8000"; // Replace with your actual API base URL
        private string _authToken;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void SetAuthToken(string token, string tokenType = "Bearer")
        {
            _authToken = token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, token);
        }

        public void ClearAuthToken()
        {
            _authToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        #region Health Check

        /// <summary>
        /// Checks if the API service is running and can connect to the database
        /// </summary>
        public async Task<HealthCheck> GetHealthCheckAsync()
        {
            return await GetAsync<HealthCheck>("/health");
        }

        #endregion

        #region Authentication

        /// <summary>
        /// Authenticates a user and returns a token
        /// </summary>
        public async Task<TokenData> LoginAsync(string email, string password)
        {
            var loginData = new { email, password };
            var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var tokenData = await response.Content.ReadFromJsonAsync<TokenData>();
                SetAuthToken(tokenData.AccessToken, tokenData.TokenType);
                return tokenData;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            throw new HttpRequestException($"Error: {response.StatusCode}");
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        public async Task<User> RegisterAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/auth/register", user);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new InvalidOperationException("Email already registered");
            }

            throw new HttpRequestException($"Error: {response.StatusCode}");
        }

        #endregion

        #region Restaurants

        /// <summary>
        /// Gets a list of all restaurants
        /// </summary>
        public async Task<List<Restaurant>> GetRestaurantsAsync(int skip = 0, int limit = 100)
        {
            var queryString = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Restaurant>>($"/restaurants{queryString}");
        }

        /// <summary>
        /// Gets a specific restaurant by ID
        /// </summary>
        public async Task<Restaurant> GetRestaurantAsync(int restaurantId)
        {
            return await GetAsync<Restaurant>($"/restaurants/{restaurantId}");
        }

        /// <summary>
        /// Gets all menus for a specific restaurant
        /// </summary>
        public async Task<List<Menu>> GetRestaurantMenusAsync(int restaurantId, int skip = 0, int limit = 100)
        {
            var queryString = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Menu>>($"/restaurants/{restaurantId}/menus{queryString}");
        }

        /// <summary>
        /// Gets all orders for a specific restaurant
        /// </summary>
        public async Task<List<Order>> GetRestaurantOrdersAsync(int restaurantId, int skip = 0, int limit = 100)
        {
            var queryString = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Order>>($"/restaurants/{restaurantId}/orders{queryString}");
        }

        #endregion

        #region Menus

        /// <summary>
        /// Gets a list of all menu items
        /// </summary>
        public async Task<List<Menu>> GetMenusAsync(int? restaurantId = null, int skip = 0, int limit = 100)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            };

            if (restaurantId.HasValue)
            {
                queryParams.Add("restaurant_id", restaurantId.Value.ToString());
            }

            var queryString = BuildQueryString(queryParams);
            return await GetAsync<List<Menu>>($"/menus{queryString}");
        }

        /// <summary>
        /// Gets quick service menu items with preparation time less than specified value
        /// </summary>
        public async Task<List<Menu>> GetQuickServiceMenusAsync(
            int maxPreparationTime = 30, 
            int? restaurantId = null, 
            int skip = 0, 
            int limit = 100)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "max_preparation_time", maxPreparationTime.ToString() },
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            };

            if (restaurantId.HasValue)
            {
                queryParams.Add("restaurant_id", restaurantId.Value.ToString());
            }

            var queryString = BuildQueryString(queryParams);
            return await GetAsync<List<Menu>>($"/menus/quick-service{queryString}");
        }

        /// <summary>
        /// Gets a specific menu item by ID
        /// </summary>
        public async Task<Menu> GetMenuAsync(int menuId)
        {
            return await GetAsync<Menu>($"/menus/{menuId}");
        }

        /// <summary>
        /// Gets all comments for a specific menu item
        /// </summary>
        public async Task<List<Comment>> GetMenuCommentsAsync(
            int menuId, 
            int? minRating = null, 
            int skip = 0, 
            int limit = 100)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            };

            if (minRating.HasValue)
            {
                queryParams.Add("min_rating", minRating.Value.ToString());
            }

            var queryString = BuildQueryString(queryParams);
            return await GetAsync<List<Comment>>($"/menus/{menuId}/comments{queryString}");
        }

        /// <summary>
        /// Gets the average rating and total number of reviews for a specific menu item
        /// </summary>
        public async Task<MenuRating> GetMenuRatingAsync(int menuId)
        {
            return await GetAsync<MenuRating>($"/menus/{menuId}/rating");
        }

        #endregion

        #region Orders

        /// <summary>
        /// Gets a specific order by ID
        /// </summary>
        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await GetAsync<Order>($"/orders/{orderId}");
        }

        /// <summary>
        /// Gets all orders for a specific user
        /// </summary>
        public async Task<List<Order>> GetUserOrdersAsync(int userId, int skip = 0, int limit = 100)
        {
            var queryString = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Order>>($"/users/{userId}/orders{queryString}");
        }

        #endregion

        #region Comments

        /// <summary>
        /// Gets a list of all comments
        /// </summary>
        public async Task<List<Comment>> GetCommentsAsync(
            int? menuId = null, 
            int? clientId = null, 
            int? rating = null, 
            int skip = 0, 
            int limit = 100)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            };

            if (menuId.HasValue)
            {
                queryParams.Add("menu_id", menuId.Value.ToString());
            }

            if (clientId.HasValue)
            {
                queryParams.Add("client_id", clientId.Value.ToString());
            }

            if (rating.HasValue)
            {
                queryParams.Add("rating", rating.Value.ToString());
            }

            var queryString = BuildQueryString(queryParams);
            return await GetAsync<List<Comment>>($"/comments{queryString}");
        }

        /// <summary>
        /// Gets a specific comment by ID
        /// </summary>
        public async Task<Comment> GetCommentAsync(int commentId)
        {
            return await GetAsync<Comment>($"/comments/{commentId}");
        }

        #endregion

        #region Menu Categories

        /// <summary>
        /// Gets a list of all menu categories
        /// </summary>
        public async Task<List<MenuCategory>> GetMenuCategoriesAsync(int skip = 0, int limit = 100)
        {
            var queryString = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<MenuCategory>>($"/menu-categories{queryString}");
        }

        /// <summary>
        /// Gets a specific menu category by ID
        /// </summary>
        public async Task<MenuCategory> GetMenuCategoryAsync(int categoryId)
        {
            return await GetAsync<MenuCategory>($"/menu-categories/{categoryId}");
        }

        /// <summary>
        /// Gets all menus for a specific category
        /// </summary>
        public async Task<List<Menu>> GetMenuCategoryMenusAsync(int categoryId, int skip = 0, int limit = 100)
        {
            var queryString = BuildQueryString(new Dictionary<string, string>
            {
                { "skip", skip.ToString() },
                { "limit", limit.ToString() }
            });

            return await GetAsync<List<Menu>>($"/menu-categories/{categoryId}/menus{queryString}");
        }

        #endregion

        #region Helper Methods

        private async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}{endpoint}");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"Resource not found at {endpoint}");
            }
            
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Authentication required");
            }
            
            throw new HttpRequestException($"Error: {response.StatusCode}");
        }

        private string BuildQueryString(Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return string.Empty;
            }

            var queryBuilder = new System.Text.StringBuilder("?");
            bool first = true;

            foreach (var parameter in parameters)
            {
                if (!first)
                {
                    queryBuilder.Append("&");
                }

                queryBuilder.Append(HttpUtility.UrlEncode(parameter.Key));
                queryBuilder.Append("=");
                queryBuilder.Append(HttpUtility.UrlEncode(parameter.Value));

                first = false;
            }

            return queryBuilder.ToString();
        }

        #endregion
    }

    // All models should be defined in a separate file
}