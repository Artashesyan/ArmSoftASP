using Homework1.Data;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController(APIContext context) : Controller
	{
		private readonly APIContext _context = context;

		[HttpGet]
		public JsonResult GetUser(int id)
		{
			var result = _context.Users.Find(id);

			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpPost]
		public JsonResult CreateUser(User user)
		{
			if (user.Id == 0)
			{
				_context.Users.Add(user);
			}
			else
			{
				var userInDB = _context.Posts.Find(user.Id);

				if (userInDB is null)
				{
					return new JsonResult(NotFound());
				}
			}

			_context.SaveChanges();

			return new JsonResult(Ok(user));
		}

		[HttpPut("{id}")]
		public JsonResult UpdateUser(int id, User updatedUser)
		{
			var userInDb = _context.Users.Find(id);
			if (userInDb is null)
				return new JsonResult(NotFound());

			userInDb.FirstName = updatedUser.FirstName;
			userInDb.LastName = updatedUser.LastName;
			userInDb.Email = updatedUser.Email;
			userInDb.Avatar = updatedUser.Avatar;

			_context.SaveChanges();
			return new JsonResult(Ok(userInDb));
		}

		[HttpDelete("{id}")]
		public JsonResult DeletePost(int id)
		{
			var userInDB = _context.Users.Find(id);
			if (userInDB is not null)
			{
				_context.Users.Remove(userInDB);
				_context.SaveChanges();
			}

			return new JsonResult(NoContent());
		}
	}
}
