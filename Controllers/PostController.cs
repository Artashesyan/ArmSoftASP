using Homework1.Data;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PostController(JsonPlaceholderClient client) : Controller
	{
		private readonly JsonPlaceholderClient _client = client;

		[HttpGet("{id}")]
		public async Task<JsonResult> GetPost(int id)
		{
			var result = await _client.GetPostById(id);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpGet]
		public async Task<JsonResult> GetPost(int userId, string title)
		{
			var result = await _client.GetPostByUserAndTitle(userId, title);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpPost]
		public async Task<JsonResult> CreatePost(Post post)
		{
			var result = await _client.CreatePost(post);
			return result is null ? new JsonResult(BadRequest()) : new JsonResult(Ok(result));
		}

		[HttpPut("{id}")]
		public async Task<JsonResult> UpdatePost(int id, Post updatedPost)
		{
			var result = await _client.UpdatePost(id, updatedPost);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpDelete("{id}")]
		public async Task<JsonResult> DeletePost(int id)
		{
			var success = await _client.DeletePost(id);
			return success ? new JsonResult(NoContent()) : new JsonResult(NotFound());
		}
	}
}
