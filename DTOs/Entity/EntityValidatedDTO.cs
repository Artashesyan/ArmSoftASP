using Homework1.Validators;
using System.ComponentModel.DataAnnotations;

namespace Homework1.DTOs.Entity
{
	public class EntityValidatedDTO
	{
		[Required]
		[MinLength(3)]
		public string Username { get; set; } = default!;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = default!;

		[Required]
		[PasswordStrength]
		public string Password { get; set; } = default!;

		[PastDate]
		public string DateOfBirth { get; set; } = default!;

		[Range(0, int.MaxValue)]
		public int Quantity { get; set; }

		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		[Range(0, 49)]
		public int Amount { get; set; }
	}

}
