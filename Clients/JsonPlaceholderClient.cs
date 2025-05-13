using Homework1.Clients.Interfaces;
using Homework1.Models;
using Homework1.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Homework1.Clients
{
    public class JsonPlaceholderClient : IJsonPlaceholderClient
	{
        private readonly HttpClient _httpClient;
		private readonly JsonPlaceholderOptions _options;

		public JsonPlaceholderClient(HttpClient httpClient, IOptions<JsonPlaceholderOptions> options)
		{
			_httpClient = httpClient;
			_options = options.Value;

			_httpClient.BaseAddress = new Uri(_options.BaseUrl);
		}

		public async Task<IEnumerable<Post?>?> GetPostsAsync()
		{
			var response = await _httpClient.GetFromJsonAsync<IEnumerable<Post>>("posts");
			return response;
		}

		public async Task<Post?> GetPostByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Post?>($"posts/{id}");

		public async Task<Post?> GetPostByUserIdAndTitleAsync(int userId, string title)
		{
			var query = new Dictionary<string, string?>
			{
				["userId"] = userId.ToString(),
				["title"] = title
			};

			var uri = QueryHelpers.AddQueryString("posts", query);
			var posts = await _httpClient.GetFromJsonAsync<List<Post?>>(uri);
			return posts?.FirstOrDefault();
		}

		public async Task<Post?> CreatePostAsync(Post post)
        {
            var response = await _httpClient.PostAsJsonAsync("posts", post);
            return await response.Content.ReadFromJsonAsync<Post?>();
        }

        public async Task<Post?> UpdatePostAsync(int id, Post updatedPost)
        {
            var response = await _httpClient.PutAsJsonAsync($"posts/{id}", updatedPost);
            return await response.Content.ReadFromJsonAsync<Post?>();
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"posts/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
