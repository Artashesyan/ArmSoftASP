using Homework1.DTOs.User;

namespace Homework1.Services.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<UserReadDTO>> GetAllUsersAsync();
		Task<UserReadDTO?> GetUserByIdAsync(int id);
		Task<UserReadDTO?> GetUserByNameAsync(string firstName, string lastName);
		Task<UserReadDTO> CreateUserAsync(UserCreateDTO dto);
		Task<bool> DeleteUserAsync(int id);
		Task<UserReadDTO?> UpdateUserAsync(int id, UserUpdateDTO dto);
	}
}
