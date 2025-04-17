using Homework1.Data;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework1.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UserController(ReqResClient client) : Controller
	{
		private readonly ReqResClient _client = client;

		[HttpGet]
		public async Task<JsonResult> GetUser(int id)
		{
			var result = await _client.GetUser(id);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpPost]
		public async Task<JsonResult> CreateUser(User user)
		{
			var result = await _client.CreateUser(user);
			return result is null ? new JsonResult(BadRequest()) : new JsonResult(Ok(result));
		}

		[HttpPut("{id}")]
		public async Task<JsonResult> UpdateUser(int id, User updatedUser)
		{
			var result = await _client.UpdateUser(id, updatedUser);
			return result is null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
		}

		[HttpDelete("{id}")]
		public async Task<JsonResult> DeleteUser(int id)
		{
			var success = await _client.DeleteUser(id);
			return success ? new JsonResult(NoContent()) : new JsonResult(NotFound());
		}
	}
}
