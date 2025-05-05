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
		public async Task<ActionResult<User>> GetUser(int id)
		{
			var result = await _client.GetUser(id);
			return result is null ? NotFound() : Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<User>> CreateUser(User user)
		{
			var result = await _client.CreateUser(user);
			return result is null
				? BadRequest()
				: Created(string.Empty, result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<User>> UpdateUser(int id, User updatedUser)
		{
			var result = await _client.UpdateUser(id, updatedUser);
			return result is null ? NotFound() : Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<User>> DeleteUser(int id)
		{
			await _client.DeleteUser(id);
			return NoContent();
		}
	}
}
