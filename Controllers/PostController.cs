using Homework1.Data;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PostController(APIContext context) : Controller
	{
		private readonly APIContext _context = context;

		[HttpGet("{id}")]
		public JsonResult GetPost(int id)
		{
			var result = _context.Posts.Find(id);

			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpGet]
		public JsonResult GetPost(int userId, string title)
		{
			var result = _context.Posts.FirstOrDefault(p => p.UserId == userId && p.Title == title);

			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpPost]
		public JsonResult CreatePost(Post post)
		{
			if (post.Id == 0)
			{
				_context.Posts.Add(post);
			}
			else
			{
				var postInDB = _context.Posts.Find(post.Id);

				if (postInDB is null)
				{
					return new JsonResult(NotFound());
				}
			}

			_context.SaveChanges();

			return new JsonResult(Ok(post));
		}

		[HttpPut("{id}")]
		public JsonResult UpdatePost(int id, Post updatedPost)
		{
			var postInDb = _context.Posts.Find(id);
			if (postInDb is null)
				return new JsonResult(NotFound());

			postInDb.UserId = updatedPost.UserId;
			postInDb.Title = updatedPost.Title;
			postInDb.Body = updatedPost.Body;

			_context.SaveChanges();
			return new JsonResult(Ok(postInDb));
		}

		[HttpDelete("{id}")]
		public JsonResult DeletePost(int id)
		{
			var postInDB = _context.Posts.Find(id);
			if (postInDB is not null)
			{
				_context.Posts.Remove(postInDB);
				_context.SaveChanges();
			}
			
			return new JsonResult(NoContent());
		}
	}
}
