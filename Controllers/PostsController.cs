using Homework1.Clients;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PostsController(JsonPlaceholderClient client) : Controller
	{
		private readonly JsonPlaceholderClient _client = client;

		[HttpGet("{id}")]
		public async Task<ActionResult> GetPost(int id)
		{
			var result = await _client.GetPostById(id);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpGet]
		public async Task<ActionResult> GetPost(int userId, string title)
		{
			var result = await _client.GetPostByUserAndTitle(userId, title);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpPost]
		public async Task<ActionResult> CreatePost(Post post)
		{
			var result = await _client.CreatePost(post);
			return result is null
							? new JsonResult(BadRequest())
							: new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdatePost(int id, Post updatedPost)
		{
			var result = await _client.UpdatePost(id, updatedPost);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeletePost(int id)
		{
			await _client.DeletePost(id);
			return NoContent();
		}
	}
}
