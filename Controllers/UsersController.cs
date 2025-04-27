using Homework1.Clients;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UsersController(ReqResClient client) : Controller
	{
		private readonly ReqResClient _client = client;

		[HttpGet]
		public async Task<ActionResult> GetUser(int id)
		{
			var result = await _client.GetUser(id);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpPost]
		public async Task<ActionResult> CreateUser(User user)
		{
			var result = await _client.CreateUser(user);
			return result is null 
							? new JsonResult(BadRequest()) 
							: new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateUser(int id, User updatedUser)
		{
			var result = await _client.UpdateUser(id, updatedUser);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteUser(int id)
		{
			await _client.DeleteUser(id);
			return NoContent();
		}
	}
}
