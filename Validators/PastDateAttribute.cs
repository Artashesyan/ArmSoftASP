using System.ComponentModel.DataAnnotations;

namespace Homework1.Validators
{
	public class PastDateAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object? value, ValidationContext context)
		{
			if (value is not string dateStr)
				return new ValidationResult("The date is empty.");

			if (!DateOnly.TryParseExact(dateStr, "yyyy-MM-dd", out var date))
				return new ValidationResult("Date must be in yyyy-mm-dd format.");

			return date < DateOnly.FromDateTime(DateTime.Today) ? 
				ValidationResult.Success : new ValidationResult("Date must be in the past.");
		}
	}
}
