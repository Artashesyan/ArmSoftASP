namespace Homework1.Models
{
	public class Entity
	{
		public int Id { get; set; }
		public required string Username { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
		public required	string DateOfBirth { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public int Amount { get; set; }
	}
}
