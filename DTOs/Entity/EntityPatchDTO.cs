using Homework1.Validators;
using System.ComponentModel.DataAnnotations;

namespace Homework1.DTOs.Entity
{
	public class EntityPatchDTO : EntityValidatedDTO
	{
		[MinLength(3)]
		public new string? Username { get; set; }

		[EmailAddress]
		public new string? Email { get; set; }

		[PasswordStrength]
		public new string? Password { get; set; }

		[PastDate]
		public new string? DateOfBirth { get; set; }

		[Range(0, int.MaxValue)]
		public new int? Quantity { get; set; }

		[Range(0, double.MaxValue)]
		public new decimal? Price { get; set; }

		[Range(0, 49)]
		public new int? Amount { get; set; }
	}
}
