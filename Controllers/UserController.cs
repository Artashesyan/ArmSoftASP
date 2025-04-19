using Homework1.Data;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Homework1.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UserController(ReqResClient client, ILogger<UserController> logger) : Controller
	{
		private readonly ReqResClient _client = client;
		private static readonly List<UserDto> _users = [];
		private readonly ILogger<UserController> _logger = logger;

		[HttpPost("save")]
		public IActionResult SaveUser([FromBody] UserDto user)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (_users.Any(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
				return BadRequest(new { message = "A user with that username already exists." });

			_users.Add(user);

			_logger.LogInformation("User '{Username}' was created successfully at {Time}.", user.Username, DateTime.UtcNow);

			return Ok(new { message = "User saved successfully." });
		}


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
