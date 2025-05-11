using Homework1.DTOs.Post;
using Homework1.Models;
using Homework1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PostsController(IPostService service) : ControllerBase
	{
		private readonly IPostService _service = service;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PostReadDTO>>> GetAll() => Ok(await _service.GetAllPostsAsync());

		[HttpGet("{id:int}")]
		public async Task<ActionResult<PostReadDTO>> Get(int id)
		{
			var result = await _service.GetPostByIdAsync(id);
			return result is null ? NotFound() : Ok(result);
		}

		[HttpGet("by-userId-title")]
		public async Task<ActionResult<PostReadDTO>> Get(int userId, string title)
		{
			var result = await _service.GetPostByUserIdAndTitleAsync(userId, title);
			return result is null ? NotFound() : Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<PostReadDTO>> Create(PostCreateDTO postDTO)
		{
			var result = await _service.CreatePostAsync(postDTO);
			return result is null
				? BadRequest()
				: Created(string.Empty, result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<PostReadDTO>> Update(int id, PostUpdateDTO updatedPostDTO)
		{
			var result = await _service.UpdatePostAsync(id, updatedPostDTO);
			return result is null ? NotFound() : Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<PostReadDTO>> Delete(int id)
		{
			await _service.DeletePostAsync(id);
			return NoContent();
		}
	}
}
