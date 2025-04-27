using Homework1.Models;
using Homework1.Options;
using Microsoft.Extensions.Options;

namespace Homework1.Clients
{
	public class ReqResClient
    {
        private readonly HttpClient _http;
		private readonly ReqResOptions _options;

		public ReqResClient(HttpClient http, IOptions<ReqResOptions> options)
		{
			_http = http;
			_options = options.Value;

			_http.BaseAddress = new Uri(_options.BaseUrl);
		}

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
