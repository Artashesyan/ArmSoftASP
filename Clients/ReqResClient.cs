using Homework1.Clients.Interfaces;
using Homework1.Models;
using Homework1.Options;
using Microsoft.Extensions.Options;

namespace Homework1.Clients
{
	public class ReqResClient : IReqResClient
    {
        private readonly HttpClient _httpClient;
		private readonly ReqResOptions _options;

		public ReqResClient(HttpClient httpClient, IOptions<ReqResOptions> options)
		{
			_httpClient = httpClient;
			_options = options.Value;

			_httpClient.BaseAddress = new Uri(_options.BaseUrl);
		}

		public async Task<IEnumerable<User>> GetUsersAsync()
		{
			var response = await _httpClient.GetFromJsonAsync<ApiResponse<User>>("users?page=2");
			return response?.Data;
		}

		public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiSingleResponse<User>>($"users/{id}");
            return response?.Data;
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("users", user);
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"users/{id}", user);
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"users/{id}");
            return response.IsSuccessStatusCode;
        }
    }

	class ApiResponse<T>
	{
		public required List<T> Data { get; set; }
	}

	class ApiSingleResponse<T>
	{
		public required T Data { get; set; }
	}
}
