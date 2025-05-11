using Homework1.Validators;
using System.ComponentModel.DataAnnotations;

namespace Homework1.DTOs.Entity
{
	public class EntityPatchDTO
	{
		[MinLength(3)]
		public string? Username { get; set; }

		[EmailAddress]
		public string? Email { get; set; }

		[PasswordStrength]
		public string? Password { get; set; }

		[PastDate]
		public string? DateOfBirth { get; set; }

		[Range(1, int.MaxValue)]
		public int? Quantity { get; set; }

		public decimal? Price { get; set; }

		[Range(0, 49)]
		public int? Amount { get; set; }
	}
}
