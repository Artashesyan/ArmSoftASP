namespace Homework1.DTOs.User
{
	public class UserUpdateDTO
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required	string Avatar { get; set; }
	}
}
