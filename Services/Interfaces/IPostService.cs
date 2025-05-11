using Homework1.DTOs.Post;

namespace Homework1.Services.Interfaces
{
	public interface IPostService
	{
		Task<IEnumerable<PostReadDTO>> GetAllPostsAsync();
		Task<PostReadDTO> GetPostByIdAsync(int id);
		Task<PostReadDTO?> GetPostByUserIdAndTitleAsync(int id, string title);
		Task<PostReadDTO> CreatePostAsync(PostCreateDTO dto);
		Task<bool> DeletePostAsync(int id);
		Task<PostReadDTO> UpdatePostAsync(int id, PostUpdateDTO dto);
	}
}
