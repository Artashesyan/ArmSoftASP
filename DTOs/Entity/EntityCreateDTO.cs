namespace Homework1.DTOs.Entity
{
	public class EntityCreateDTO
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string DateOfBirth { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public int Amount { get; set; }
	}
}
