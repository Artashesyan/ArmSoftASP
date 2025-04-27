using Homework1.Models;
using Homework1.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Homework1.Clients
{
    public class JsonPlaceholderClient
    {
        private readonly HttpClient _http;
		private readonly JsonPlaceholderOptions _options;

		public JsonPlaceholderClient(HttpClient http, IOptions<JsonPlaceholderOptions> options)
		{
			_http = http;
			_options = options.Value;

			_http.BaseAddress = new Uri(_options.BaseUrl);
		}

		public async Task<Post?> GetPostById(int id) =>
            await _http.GetFromJsonAsync<Post>($"posts/{id}");

		public async Task<Post?> GetPostByUserAndTitle(int userId, string title)
		{
			var query = new Dictionary<string, string?>
			{
				["userId"] = userId.ToString(),
				["title"] = title
			};

			var uri = QueryHelpers.AddQueryString("posts", query);
			var posts = await _http.GetFromJsonAsync<List<Post>>(uri);
			return posts?.FirstOrDefault();
		}

		public async Task<Post?> CreatePost(Post post)
        {
            var response = await _http.PostAsJsonAsync("posts", post);
            return await response.Content.ReadFromJsonAsync<Post>();
        }

        public async Task<Post?> UpdatePost(int id, Post updated)
        {
            var response = await _http.PutAsJsonAsync($"posts/{id}", updated);
            return await response.Content.ReadFromJsonAsync<Post>();
        }

        public async Task<bool> DeletePost(int id)
        {
            var response = await _http.DeleteAsync($"posts/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
