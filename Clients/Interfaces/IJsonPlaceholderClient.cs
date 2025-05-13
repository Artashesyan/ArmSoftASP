using Homework1.Models;

namespace Homework1.Clients.Interfaces
{
	public interface IJsonPlaceholderClient
	{
		Task<IEnumerable<Post?>?> GetPostsAsync();
		Task<Post?> GetPostByIdAsync(int id);
		Task<Post?> GetPostByUserIdAndTitleAsync(int id, string title);
		Task<Post?> CreatePostAsync(Post post);
		Task<bool> DeletePostAsync(int id);
		Task<Post?> UpdatePostAsync(int id, Post post);
	}
}
