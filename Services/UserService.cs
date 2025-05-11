using AutoMapper;
using Homework1.Clients.Interfaces;
using Homework1.DTOs.User;
using Homework1.Models;
using Homework1.Services.Interfaces;

namespace Homework1.Services
{
	public class UserService : IUserService
	{
		private readonly IReqResClient _client;
		private readonly IMapper _mapper;

		public UserService(IReqResClient client, IMapper mapper)
		{
			_client = client;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UserReadDTO>> GetAllUsersAsync()
		{
			var users = await _client.GetUsersAsync();
			return _mapper.Map<IEnumerable<UserReadDTO>>(users);
		}

		public async Task<UserReadDTO?> GetUserByIdAsync(int id)
		{
			var user = await _client.GetUserByIdAsync(id);
			return user is null ? null : _mapper.Map<UserReadDTO?>(user);
		}

		public async Task<bool> GetUserByNameAsync(string firstName, string lastName)
		{
			var users = await _client.GetUsersAsync();
			foreach (var user in users) 
			{
				if (user.FirstName == firstName && user.LastName == lastName)
				{
					return true;
				}
			}

			return false;
		}

		public async Task<UserReadDTO> CreateUserAsync(UserCreateDTO dto)
		{
			var user = _mapper.Map<User>(dto);
			var created = await _client.CreateUserAsync(user);
			return _mapper.Map<UserReadDTO>(created);
		}

		public async Task<UserReadDTO?> UpdateUserAsync(int id, UserUpdateDTO userUpdateDTO)
		{
			var user = _mapper.Map<User>(userUpdateDTO);
			var updated = await _client.UpdateUserAsync(id, user);
			return updated is null ? null : _mapper.Map<UserReadDTO>(updated);
		}

		public async Task<bool> DeleteUserAsync(int id)
		{
			return await _client.DeleteUserAsync(id);
		}
	}
}
