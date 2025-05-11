using Homework1.Models;

namespace Homework1.Clients.Interfaces
{
	public interface IReqResClient
	{
		Task<IEnumerable<User>> GetUsersAsync();
		Task<User> GetUserByIdAsync(int id);
		Task<User?> CreateUserAsync(User user);
		Task<bool> DeleteUserAsync(int id);
		Task<User?> UpdateUserAsync(int id, User user);
	}
}
