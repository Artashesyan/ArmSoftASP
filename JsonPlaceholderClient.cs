using Homework1.Models;

namespace Homework1.Data
{
	public class JsonPlaceholderClient(HttpClient http)
	{
		private readonly HttpClient _http = http;

		public async Task<Post?> GetPostById(int id) =>
			await _http.GetFromJsonAsync<Post>($"posts/{id}");

		public async Task<Post?> GetPostByUserAndTitle(int userId, string title)
		{
			var posts = await _http.GetFromJsonAsync<List<Post>>($"posts?userId={userId}");
			return posts?.FirstOrDefault(p => p.Title == title);
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
