using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Homework1.Models
{
	public class UserDto
	{
		[Required]
		[MinLength(3, ErrorMessage = "Username must be at least 3 characters.")]
		public required string Username { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Email is not valid.")]
		public required string Email { get; set; }

		[Required]
		[MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
		[CustomValidation(typeof(UserDto), nameof(ValidatePassword))]
		public required string Password { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[CustomValidation(typeof(UserDto), nameof(ValidateDateOfBirth))]
		public DateTime DateOfBirth { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
		public int Quantity { get; set; }

		[Required]
		[Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Price must be a decimal value.")]
		public decimal Price { get; set; }

		[Required]
		[Range(0, 49.99, ErrorMessage = "Amount must be less than 50.")]
		public decimal Amount { get; set; }

		public static ValidationResult ValidatePassword(string password, ValidationContext context)
		{
			var instance = (UserDto)context.ObjectInstance;
			var hasUpper = Regex.IsMatch(password, "[A-Z]");
			var hasLower = Regex.IsMatch(password, "[a-z]");
			var hasDigit = Regex.IsMatch(password, "[0-9]");
			var hasSymbol = Regex.IsMatch(password, @"[\W_]");
			var hasArmenian = Regex.IsMatch(password, "[\u0531-\u058F]");
			var containsUsername = !string.IsNullOrEmpty(instance.Username) && password.ToLower().Contains(instance.Username.ToLower());

			if (!hasUpper)
			{
				return new ValidationResult("Password must have at least one uppercase letter.");
			}

			if (!hasLower)
			{
				return new ValidationResult("Password must have at least one lowercase letter.");
			}

			if (!hasDigit)
			{
				return new ValidationResult("Password must have at least one digit.");
			}

			if (!hasSymbol)
			{
				return new ValidationResult("Password must have at least one symbol.");
			}

			if (!hasArmenian)
			{
				return new ValidationResult("Password must have at least one Armenian letter.");
			}

			if (containsUsername)
			{
				return new ValidationResult("Password must not contain the username.");
			}


			return ValidationResult.Success;
		}

		public static ValidationResult ValidateDateOfBirth(DateTime dob, ValidationContext context)
		{
			return dob < DateTime.Today ? ValidationResult.Success : new ValidationResult("Date of birth must be in the past.");
		}
	}
}