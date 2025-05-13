using Homework1.Clients.Interfaces;
using Homework1.Models;
using Homework1.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

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
			_httpClient.DefaultRequestHeaders.Add("x-api-key", _options.ApiKey);
		}

		public async Task<IEnumerable<User>> GetUsersAsync()
		{
			var allUsers = new List<User>();
			int page = 1;
			int totalPages;

			do
			{
				var response = await _httpClient.GetFromJsonAsync<ApiResponse<User>>("users?page=" + page);

				if (response?.Data is not null)
				{
					allUsers.AddRange(response.Data);
					totalPages = response.TotalPages;
				}
				else
				{
					break;
				}

				++page;
			}
			while (page <= totalPages);

			return allUsers;
		}

		public async Task<User?> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiSingleResponse<User>>($"users/{id}");
            return response?.Data;
        }

		public async Task<User?> GetUserByNameAsync(string firstName, string lastName)
		{
			var users = await GetUsersAsync();
			foreach (var user in users)
			{
				if (user.FirstName == firstName && user.LastName == lastName)
				{
					return user;
				}
			}

			return null;
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
		[JsonPropertyName("total_pages")]
		public int TotalPages { get; set; }
	}

	class ApiSingleResponse<T>
	{
		public required T Data { get; set; }
	}
}
