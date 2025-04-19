using Homework1.Models;
using System.Net.Http.Json;

namespace Homework1.Data
{
	public class ReqResClient(HttpClient http)
	{
		private readonly HttpClient _http = http;

		public async Task<User?> GetUser(int id)
		{
			var response = await _http.GetFromJsonAsync<SingleUserResponse>($"api/users/{id}");
			return response?.Data;
		}

		public async Task<User?> CreateUser(User user)
		{
			var response = await _http.PostAsJsonAsync("api/users", user);
			return await response.Content.ReadFromJsonAsync<User>();
		}

		public async Task<User?> UpdateUser(int id, User user)
		{
			var response = await _http.PutAsJsonAsync($"api/users/{id}", user);
			return await response.Content.ReadFromJsonAsync<User>();
		}

		public async Task<bool> DeleteUser(int id)
		{
			var response = await _http.DeleteAsync($"api/users/{id}");
			return response.IsSuccessStatusCode;
		}
	}

	public class SingleUserResponse
	{
		public User? Data { get; set; }
	}
}
