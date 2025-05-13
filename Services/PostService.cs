using AutoMapper;
using Homework1.Clients.Interfaces;
using Homework1.DTOs.Post;
using Homework1.Models;
using Homework1.Services.Interfaces;

namespace Homework1.Services
{
	public class PostService(IJsonPlaceholderClient client, IMapper mapper) : IPostService
	{
		private readonly IJsonPlaceholderClient _client = client;
		private readonly IMapper _mapper = mapper;

		public async Task<IEnumerable<PostReadDTO>> GetAllPostsAsync()
		{
			var posts = await _client.GetPostsAsync();
			return _mapper.Map<IEnumerable<PostReadDTO>>(posts);
		}

		public async Task<PostReadDTO> GetPostByIdAsync(int id)
		{
			var post = await _client.GetPostByIdAsync(id);
			return _mapper.Map<PostReadDTO>(post);
		}

		public async Task<PostReadDTO?> GetPostByUserIdAndTitleAsync(int userId, string title)
		{
			var post = await _client.GetPostByUserIdAndTitleAsync(userId, title);
			return post == null ? null : _mapper.Map<PostReadDTO>(post);
		}

		public async Task<PostReadDTO> CreatePostAsync(PostCreateDTO dto)
		{
			var post = _mapper.Map<Post>(dto);
			var created = await _client.CreatePostAsync(post);
			return _mapper.Map<PostReadDTO>(created);
		}

		public async Task<PostReadDTO> UpdatePostAsync(int id, PostUpdateDTO postUpdateDTO)
		{
			var post = _mapper.Map<Post>(postUpdateDTO);
			var updatedPost = await _client.UpdatePostAsync(id, post);
			return _mapper.Map<PostReadDTO>(updatedPost);
		}

		public async Task<bool> DeletePostAsync(int id)
		{
			return await _client.DeletePostAsync(id);
		}
	}
}
