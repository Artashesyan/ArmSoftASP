using System.Text.Json.Serialization;

namespace Homework1.Models
{
	public class User
	{
		public int Id { get; set; }
		[JsonPropertyName("first_name")]
		public required string FirstName { get; set; }
		[JsonPropertyName("last_name")]
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required string Avatar { get; set; }
	}
}
