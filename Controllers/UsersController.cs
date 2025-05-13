using Homework1.Clients;
using Homework1.DTOs.User;
using Homework1.Models;
using Homework1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Homework1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController(IUserService service, ILogger<UsersController> logger) : ControllerBase
	{
		private readonly IUserService _service = service;
		private readonly ILogger<UsersController> _logger = logger;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAll() => Ok(await _service.GetAllUsersAsync());

		[HttpGet("{id:int}")]
		public async Task<ActionResult<UserReadDTO>> Get(int id)
		{
			var user = await _service.GetUserByIdAsync(id);
			return user == null ? NotFound() : Ok(user);
		}

		[HttpGet("by-name")]
		public async Task<ActionResult<UserReadDTO>> Get(string firstName, string lastName)
		{
			var user = await _service.GetUserByNameAsync(firstName, lastName);
			return user == null ? NotFound() : Ok(user);
		}

		[HttpPost]
		public async Task<ActionResult<UserReadDTO>> Create(UserCreateDTO userDTO)
		{
			if(await _service.GetUserByNameAsync(userDTO.FirstName, userDTO.LastName) is not null)
			{
				return BadRequest(new { message = "A user with that name already exists." });
			}

			var result = await _service.CreateUserAsync(userDTO);
			if (result is null)
			{
				return BadRequest();
			}

			_logger.LogInformation("User '{FirstName} {LastName}' was created successfully at {Time}.", 
				userDTO.FirstName, userDTO.LastName, DateTime.UtcNow);

			return Created(string.Empty, result);
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult<UserReadDTO>> Update(int id, UserUpdateDTO dto)
		{
			var updated = await _service.UpdateUserAsync(id, dto);
			return updated == null ? NotFound() : Ok(updated);
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<UserReadDTO>> Delete(int id)
		{
			await _service.DeleteUserAsync(id);
			return NoContent();
		}
	}
}
