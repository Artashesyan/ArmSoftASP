using Homework1.DTOs.Entity;
using System.ComponentModel.DataAnnotations;

namespace Homework1.Validators
{
	public class PasswordStrengthAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext context)
		{
			if (value is not string password) return new ValidationResult("Password required.");
			var entity = (EntityValidatedDTO)context.ObjectInstance;

			if (password.Length < 6)
			{
				return new ValidationResult("Password must have at least 6 characters.");
			}

			if (!password.Any(char.IsUpper))
			{
				return new ValidationResult("Password must have at least one uppercase letter.");
			}

			if (!password.Any(char.IsLower))
			{
				return new ValidationResult("Password must have at least one lowercase letter.");
			}

			if (!password.Any(char.IsDigit))
			{
				return new ValidationResult("Password must have at least one number.");
			}

			if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
			{
				return new ValidationResult("Password must have at least one symbol.");
			}

			if (!password.Any(ch => ch >= 0x0530 && ch <= 0x058F))
			{
				return new ValidationResult("Password must have at least one Armenian letter.");
			}

			if (entity.Username is not null && password.Contains(entity.Username, StringComparison.OrdinalIgnoreCase))
			{
				return new ValidationResult("Password mustn't contain the Username.");
			}

			return ValidationResult.Success;
		}
	}
}
